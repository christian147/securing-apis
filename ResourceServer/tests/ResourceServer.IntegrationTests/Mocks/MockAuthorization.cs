using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceServer.Constants;

namespace ResourceServer.IntegrationTests.Mocks
{
    public static class MockAuthorization
    {
        public static IServiceCollection AddMockAuthorization(this IServiceCollection services)
        {
            return services;
        }
    }
}
