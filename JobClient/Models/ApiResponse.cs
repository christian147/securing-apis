using System.Text.Json.Serialization;

namespace JobClient.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
