namespace TradeSphere.Application.DTOs.AuthDto
{
    public class UserResultDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public string? RefreshToken { get; set; }        // ✨ Add this
        public DateTime? RefreshTokenExpiration { get; set; } // ✨ Add this
    }
}
