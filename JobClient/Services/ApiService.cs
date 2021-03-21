using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using IdentityModel.Client;
using JobClient.Models;

namespace JobClient.Services
{
    public class ApiService
    {
        private const string ApiUrl = "http://localhost:5000/api/policies";
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> Get(TokenResponse token, string path)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{ApiUrl}/{path}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

            var response = await _httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse
                {
                    Message = $"{response.StatusCode} - {content}"
                };
            }

            return JsonSerializer.Deserialize<ApiResponse>(content);
        }
    }
}
