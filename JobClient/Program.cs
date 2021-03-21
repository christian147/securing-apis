using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using JobClient.Models;
using JobClient.Services;

namespace JobClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            var authService = new AuthenticationService(httpClient);
            var token = await authService.GetAccessToken();

            PrintToken(token);

            var apiService = new ApiService(httpClient);
            var response = await apiService.Get(token, "migrator");
            PrintApiRespone(response);

            response = await apiService.Get(token, "authorized");
            PrintApiRespone(response);
        }

        private static void PrintToken(TokenResponse token)
        {
            Console.WriteLine($"token_type: {token.TokenType}");
            Console.WriteLine($"access_token: {token.AccessToken}");
            Console.WriteLine($"Press any key to continue calling Resource Server:");
            Console.ReadLine();
        }
        private static void PrintApiRespone(ApiResponse response)
        {
            Console.WriteLine($"api_response: {response.Message}");
            Console.ReadLine();
        }
    }
}
