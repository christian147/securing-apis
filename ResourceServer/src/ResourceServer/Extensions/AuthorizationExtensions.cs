using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ResourceServer.Extensions
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }
    }
}
