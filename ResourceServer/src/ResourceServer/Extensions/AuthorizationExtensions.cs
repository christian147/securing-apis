using System;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceServer.Constants;
using ResourceServer.Requirements;

namespace ResourceServer.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationHandler, ApiKeyRequirementHandler>();
            return services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireRole(Role.Administrator, Role.User)
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy(Policy.SendEmails, policy =>
                {
                    policy
                        .RequireClaim(JwtClaimTypes.EmailVerified, "true")
                        .RequireClaim(JwtClaimTypes.Scope, "email.write")
                        .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.OlderThan18, policy =>
                {
                    policy
                    .RequireAssertion(ctx => IsOlderThan(ctx, 18))
                    .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.Migration, policy =>
                {
                    policy.RequireClaim(JwtClaimTypes.Scope, "migration")
                    .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.ApiKey, policy =>
                {
                    policy.AddRequirements(new ApiKeyRequirement(configuration["ApiKey"]));
                });
            });
        }

        private static bool IsOlderThan(AuthorizationHandlerContext context, int minimumAge)
        {
            if (context.User == null)
            {
                return false;
            }

            var birthday = DateTime.ParseExact(context.User.FindFirst(JwtClaimTypes.BirthDate).Value, "MM/dd/yyyy", null);
            return birthday.AgeInYears() > minimumAge;
        }
    }
}
