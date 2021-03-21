using System;
using IdentityModel;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ResourceServer.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment environment)
        {
            services
                .AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
                .AddOAuth2Introspection(options =>
                {
                    options.Authority = configuration["Authority"];
                    options.ClientId = configuration["Audience"];
                    options.ClientSecret = configuration["ClientSecret"];
                    options.SaveToken = true;
                    options.EnableCaching = true;
                    options.CacheDuration = new TimeSpan(0, 0, 10);
                    options.RoleClaimType = JwtClaimTypes.Role;
                    options.NameClaimType = JwtClaimTypes.Name;
                });

            return services;
        }
    }
}
