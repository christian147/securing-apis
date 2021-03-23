using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using ResourceServer.IntegrationTests.Provider;

namespace ResourceServer.IntegrationTests.Mocks
{
    //https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-5.0#mock-authentication
    public class MockAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string AuthenticationScheme = "TestAuth";
        private readonly IEnumerable<Claim> _claims;

        public MockAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITestClaimsProvider testClaimsProvider) : base(options, logger, encoder, clock)
        {
            _claims = testClaimsProvider.GetClaims();
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Context.Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(authHeader))
            {
                return null;
            }

            var identity = new ClaimsIdentity(_claims, AuthenticationScheme,
                JwtClaimTypes.Name, JwtClaimTypes.Role);
            var principal = new ClaimsPrincipal(identity);

            if (principal == null)
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var ticket = new AuthenticationTicket(principal, AuthenticationScheme);
            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}