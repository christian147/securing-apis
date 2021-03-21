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
            return services;
        }
    }
}
