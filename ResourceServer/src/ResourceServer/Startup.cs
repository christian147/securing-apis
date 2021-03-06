using System;
using IdentityModel;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceServer.Constants;
using ResourceServer.Requirements;

namespace ResourceServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        });
                })
                .AddControllers();

            services
             .AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
             .AddOAuth2Introspection(options =>
             {
                 options.Authority = "http://localhost:5001";
                 options.ClientId = "api";
                 options.ClientSecret = "secret";
                 options.SaveToken = true;
                 options.EnableCaching = true;
                 options.CacheDuration = new TimeSpan(0, 0, 30);
                 options.RoleClaimType = JwtClaimTypes.Role;
                 options.NameClaimType = JwtClaimTypes.Name;
             });
            services.AddDistributedMemoryCache();

            services.AddSingleton<IAuthorizationHandler, ApiKeyRequirementHandler>();
            services.AddAuthorization(options =>
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

                options.AddPolicy(Policy.Migration, policy =>
                {
                    policy.RequireClaim(JwtClaimTypes.Scope, "migration")
                    .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.ApiKey, policy =>
                {
                    policy.AddRequirements(new ApiKeyRequirement("81f8e9e8-d031-407d-8182-0c79488737a7"));
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseDeveloperExceptionPage()
                .UseCors()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
