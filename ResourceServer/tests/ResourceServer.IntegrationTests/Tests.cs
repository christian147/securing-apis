using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using ResourceServer.Constants;
using ResourceServer.IntegrationTests.Extensions;
using Xunit;

namespace ResourceServer.IntegrationTests
{
    public class Tests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _webAppFactory;

        public Tests(TestWebApplicationFactory webAppFactory)
        {
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task MockClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, Role.Administrator),
                new Claim(JwtClaimTypes.Email, "test@email.com"),
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()),
                new Claim(CustomClaimType.OrganizationId, Guid.NewGuid().ToString())
            };
            var client = _webAppFactory.CreateClientWithTestAuth(claims);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "api/claims");
            var response = await client.SendAsync(httpRequestMessage);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task MockRoleAuthorizeAttribute()
        {
            var client = _webAppFactory.CreateClientWithTestAuth(Enumerable.Empty<Claim>());

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "api/policies/administrator");
            var response = await client.SendAsync(httpRequestMessage);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
