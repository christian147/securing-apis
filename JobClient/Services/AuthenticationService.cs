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
            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(AppSettings.Authority);
            var tokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = AppSettings.ClientId,
                ClientSecret = AppSettings.ClientSecret,
                Address = discoveryDocument.TokenEndpoint,
                GrantType = AppSettings.GrantType,
                Scope = AppSettings.Scope
            };

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(tokenRequest);

            return tokenResponse;
        }
    }
}
