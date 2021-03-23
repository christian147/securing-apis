using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace JobClient.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenResponse> GetAccessToken()
        {
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync("http://localhost:5001");
            var tokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = "jobclient.clientcredentials",
                ClientSecret = "secret",
                Address = discoveryDocument.TokenEndpoint,
                GrantType = "client_credentials",
                Scope = "migration"
            };

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

            return tokenResponse;
        }
    }
}