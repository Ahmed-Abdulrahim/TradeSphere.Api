namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<AppUser> FindByEmailAsync(string email);
        Task<AppUser> FindByUserIdAsync(string userId);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        public Task<bool> CheckIfUserNameExistAsync(string userName);
        Task<AppUser> CreateAsync(AppUser user, string password);
        Task<bool> ConfirmEmailAsync(AppUser user, string token);
        Task<bool> IsEmailConfirmedAsync(AppUser user);
        Task ForgetPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<string> ChangeEmailAsync(string userId, ConfirmChangeEmailRequest emailChange);
        Task LogoutAsync(string userId);
    }
}
