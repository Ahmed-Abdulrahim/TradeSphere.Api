namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<AppUser> FindByEmailAsync(string email);
        public Task<bool> CheckIfUserNameExist(string userName);
        public Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<AppUser> CreateAsync(AppUser user, string password);

    }
}
