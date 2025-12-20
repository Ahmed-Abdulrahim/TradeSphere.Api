namespace TradeSphere.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<string> GenerateJwtToken(AppUser user);
        RefreshToken GenerateRefreshToken(int userId, bool rememberMe);
        Task<(string AccessToken, RefreshToken RefreshToken)> RefreshTokenAsync(string refreshToken);


    }
}
