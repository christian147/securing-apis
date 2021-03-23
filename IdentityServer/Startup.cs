using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var certificate = new X509Certificate2(
                Path.Combine(Environment.ContentRootPath, "certificate.pfx"), "P@ssword1+");

            services
                .AddIdentityServer()
                .AddTestUsers(Config.TestUsers.ToList())
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryClients(Config.Clients)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddSigningCredential(certificate);

            services
                .AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseDeveloperExceptionPage()
                .UseStaticFiles()
                .UseRouting()
                .UseIdentityServer()
                .UseCookiePolicy(new CookiePolicyOptions()
                {
                    MinimumSameSitePolicy = SameSiteMode.Lax
                })
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
