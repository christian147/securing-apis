using Microsoft.Extensions.DependencyInjection;

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
