using System.Text.Json.Serialization;

namespace proyecto.API.Entities.Login
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
