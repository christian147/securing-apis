using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceServer.Constants;
using ResourceServer.IntegrationTests.Mocks;

namespace ResourceServer.IntegrationTests
{
    public class TestStartup
    {
        public IConfiguration Configuration;

        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers();

            services
                .AddAuthentication(MockAuthenticationHandler.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, MockAuthenticationHandler>(
                    MockAuthenticationHandler.AuthenticationScheme, o => { });

            services.AddSingleton<IAuthorizationHandler, MockRolesAuthorizationRequirement>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.SendEmails, policy =>
                {
                    policy
                        .AddAuthenticationSchemes(MockAuthenticationHandler.AuthenticationScheme)
                        .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.Migration, policy =>
                {
                    policy
                        .AddAuthenticationSchemes(MockAuthenticationHandler.AuthenticationScheme)
                        .RequireAuthenticatedUser();
                });

                options.AddPolicy(Policy.ApiKey, policy =>
                {
                    policy
                        .AddAuthenticationSchemes(MockAuthenticationHandler.AuthenticationScheme)
                        .RequireAuthenticatedUser();
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app
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
