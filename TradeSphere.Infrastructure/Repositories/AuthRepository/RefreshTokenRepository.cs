namespace TradeSphere.Infrastructure.Repositories.AuthRepository
{
    public class RefreshTokenRepository(IUnitOfWork unitOfWork) : IRefreshTokenRepository
    {

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            var spec = new RefreshTokenSpecification(r => r.Token == token);
            return await unitOfWork.Repository<RefreshToken>().GetByIdSpec(spec);
        }


        public async Task<RefreshToken?> GetActiveTokenByUserIdAsync(int userId)
        {
            var spec = new RefreshTokenSpecification(r =>
                r.AppUserId == userId &&
                r.RevokedOn == null &&
                r.ExpireOn > DateTime.UtcNow);
            return await unitOfWork.Repository<RefreshToken>().GetByIdSpec(spec);
        }


        public async Task AddAsync(RefreshToken refreshToken)
        {
            await unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            await unitOfWork.CommitAsync();
        }

        public async Task RevokeAsync(RefreshToken refreshToken)
        {
            refreshToken.RevokedOn = DateTime.UtcNow;
            unitOfWork.Repository<RefreshToken>().Update(refreshToken);
            await unitOfWork.CommitAsync();
        }

        public async Task RevokeAllByUserIdAsync(int userId)
        {
            var spec = new RefreshTokenSpecification(r =>
                r.AppUserId == userId &&
                r.RevokedOn == null);

            var tokens = await unitOfWork.Repository<RefreshToken>().GetAllWithSpec(spec);

            foreach (var token in tokens)
            {
                token.RevokedOn = DateTime.UtcNow;
                unitOfWork.Repository<RefreshToken>().Update(token);
            }

            await unitOfWork.CommitAsync();
        }
    }
}
