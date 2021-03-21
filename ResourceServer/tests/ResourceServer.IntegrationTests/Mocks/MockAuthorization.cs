using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceServer.Constants;

namespace ResourceServer.IntegrationTests.Mocks
{
    public static class MockAuthorization
    {
        public static IServiceCollection AddMockAuthorization(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, MockRolesAuthorizationRequirement>();
            return services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.SendEmails, policy =>
                {
                    policy
                        .AddAuthenticationSchemes(MockAuthenticationHandler.AuthenticationScheme)
                        .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.OlderThan18, policy =>
                {
                    policy
                         .AddAuthenticationSchemes(MockAuthenticationHandler.AuthenticationScheme)
                         .RequireAuthenticatedUser();
                }); 
                
                options.AddPolicy(Policy.Migration, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.ApiKey, policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
