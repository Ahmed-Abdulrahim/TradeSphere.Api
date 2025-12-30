namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<AppUser> GetProfileAsync(string userId);
        Task<AppUser> UpdateProfileAsync(AppUser appUser);
        Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task RequestChangeEmailAsync(string userId, string newEmail);
    }
}
