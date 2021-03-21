using System.Threading.Tasks;
using Xunit;

namespace ResourceServer.IntegrationTests
{
    public class AuthenticationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _webAppFactory;
        private const string BasePath = "api/claims";

        public AuthenticationTests(TestWebApplicationFactory webAppFactory)
        {
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task MockClaims()
        {
        }
    }
}
