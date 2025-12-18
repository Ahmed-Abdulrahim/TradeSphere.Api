namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public Task<AppUser> FindByEmailAsync(string email);
        public Task<bool> CheckIfUserNameExist(string userName);
        public Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<AppUser> CreateAsync(AppUser user, string password);
        public Task<AppUser> FindByUserIdAsync(string userId);
        public Task<bool> ConfirmEmailAync(AppUser user, string token);
        public Task<bool> IsEmailConfirmed(AppUser user);
        public Task<bool> ChangePassword(AppUser user, string currentPassword, string newPassword);
        public Task ForgetPasswordAsync(string email);
        public Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword);
        public Task RequestChangeEmailAsync(string currentEmail, string newEmail);
        public Task<string> ChangeEmail(string userId, ConfirmChangeEmailRequest emailChange);

    }
}
