using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using ResourceServer.IntegrationTests.Mocks;
using ResourceServer.IntegrationTests.Provider;

namespace ResourceServer.IntegrationTests.Extensions
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateClientWithTestAuth<T>(this WebApplicationFactory<T> factory, IEnumerable<Claim> claims) where T : class
        {
            var client = factory.WithAuthentication(claims)
                .CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(MockAuthenticationHandler.AuthenticationScheme);

            return client;
        }

        private static WebApplicationFactory<T> WithAuthentication<T>(this WebApplicationFactory<T> factory, IEnumerable<Claim> claims) where T : class
        {
            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<ITestClaimsProvider, TestClaimsProvider>(_ => new TestClaimsProvider(claims));
                });
            });
        }
    }
}
