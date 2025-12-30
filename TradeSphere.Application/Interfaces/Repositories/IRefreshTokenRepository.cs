namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<RefreshToken?> GetActiveTokenByUserIdAsync(int userId);
        Task AddAsync(RefreshToken refreshToken);
        Task RevokeAsync(RefreshToken refreshToken);
        Task RevokeAllByUserIdAsync(int userId);
    }
}
