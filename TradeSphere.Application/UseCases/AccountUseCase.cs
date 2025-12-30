namespace TradeSphere.Application.UseCases
{
    public class AccountUseCase(IAccountRepository accountRepository, IMapper mapper, IAuthRepository authRepository)
    {
        public async Task<UserProfileDto> GetProfile(string UserId)
        {
            var user = await accountRepository.GetProfileAsync(UserId);
            if (user is null) throw new Exception("User Not Found");
            return mapper.Map<UserProfileDto>(user); ;
        }

        public async Task<UserProfileDto> UpdateProfile(string UserId, UpdateProfileDto updateProfile)
        {
            var user = await accountRepository.GetProfileAsync(UserId);
            if (user is null) throw new Exception("User Not Found");
            var updatedUser = mapper.Map(updateProfile, user);
            var result = await accountRepository.UpdateProfileAsync(updatedUser);
            return mapper.Map<UserProfileDto>(result);

        }

        public async Task<string> ChangePassword(string email, string currentPassword, string newPassword)
        {
            var user = await authRepository.FindByEmailAsync(email);
            var flag = await accountRepository.ChangePasswordAsync(user, currentPassword, newPassword);
            return "PasswordChangeSucess";

        }
        public async Task<string> RequestChangeEmail(string currentEmail, string newEmail)
        {
            await accountRepository.RequestChangeEmailAsync(currentEmail, newEmail);
            return "Email Has Sent Success";
        }
    }
}
