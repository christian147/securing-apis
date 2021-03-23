using Microsoft.Extensions.DependencyInjection;

namespace ResourceServer.IntegrationTests.Mocks
{
    public static class MockAuthentication
    {
        public static IServiceCollection AddMockAuthentication(this IServiceCollection services)
        {
            return services;
        }
    }
}
