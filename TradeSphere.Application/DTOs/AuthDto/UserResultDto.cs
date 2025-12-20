using System.Text.Json.Serialization;

namespace TradeSphere.Application.DTOs.AuthDto
{
    public class UserResultDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        [JsonIgnore]
        public string Message { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
