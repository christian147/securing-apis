using System.Collections.Generic;
using System.Security.Claims;

namespace ResourceServer.IntegrationTests.Provider
{
    public class TestClaimsProvider : ITestClaimsProvider
    {
        private readonly IEnumerable<Claim> _claims;

        public TestClaimsProvider(IEnumerable<Claim> claims)
        {
            _claims = claims;
        }

        public IEnumerable<Claim> GetClaims() => _claims;
    }

    public interface ITestClaimsProvider
    {
        IEnumerable<Claim> GetClaims();
    }
}
