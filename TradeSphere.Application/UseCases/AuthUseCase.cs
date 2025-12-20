namespace TradeSphere.Application.UseCases
{
    public class AuthUseCase(IAuthService authServices, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        public async Task<UserResultDto> RegisterUser(UserRegisterDto registerUser)
        {
            var existingUser = await userRepository.FindByEmailAsync(registerUser.Email);
            if (existingUser != null)
            {
                throw new Exception("This Email Is Already Exist");
            }

            if (await userRepository.CheckIfUserNameExist(registerUser.UserName))
            {
                throw new Exception("This UserName Is Already Exist");
            }
            var addUser = new AppUser()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber,
            };
            await userRepository.CreateAsync(addUser, registerUser.Password);
            var result = new UserResultDto()
            {
                Email = registerUser.Email,
                UserName = registerUser.UserName,
                Message = "Verify Your Email"
            };
            return result;
        }
        public async Task<UserResultDto> LoginUser(UserLoginDto loginUser)
        {
            var findUser = await userRepository.FindByEmailAsync(loginUser.Email);
            if (findUser is null) throw new Exception("InValid Email Or Password");
            var checkPassword = await userRepository.CheckPasswordAsync(findUser, loginUser.Password);
            if (!checkPassword) throw new Exception("invalid Email or Password");
            if (!await userRepository.IsEmailConfirmed(findUser))
                throw new Exception("Email not confirmed");
            var accessToken = await authServices.GenerateJwtToken(findUser);
            var refreshToken = authServices.GenerateRefreshToken(findUser.Id, loginUser.RememberMe);
            await refreshTokenRepository.AddAsync(refreshToken);

            return new UserResultDto()
            {
                Email = loginUser.Email,
                UserName = findUser.UserName,
                Token = await authServices.GenerateJwtToken(findUser),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpireOn
            };
        }
        public async Task<string> ConfirmEmail(string userId, string token)
        {
            var user = await userRepository.FindByUserIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var result = await userRepository.ConfirmEmailAync(user, token);

            return "Email confirmed successfully";
        }
        public async Task<string> ChangePassword(string email, string currentPassword, string newPassword)
        {
            var user = await userRepository.FindByEmailAsync(email);
            var flag = await userRepository.ChangePassword(user, currentPassword, newPassword);
            return "PasswordChangeSucess";

        }

        public async Task<string> ForgetPassword(string email)
        {
            await userRepository.ForgetPasswordAsync(email);
            return "Email Has Sent Success";

        }

        public async Task<string> resetPassword(ResetPasswordDto resetPassword)
        {
            var flag = await userRepository.ResetPasswordAsync(resetPassword);
            if (!flag) return null;
            return "Password Changed Success";

        }

        public async Task<string> RequestChangeEmail(string currentEmail, string newEmail)
        {
            await userRepository.RequestChangeEmailAsync(currentEmail, newEmail);
            return "Email Has Sent Success";
        }

        public async Task<string> ConfrimEmailForAfterChanging(string userId, ConfirmChangeEmailRequest emailChange)
        {
            await userRepository.ChangeEmail(userId, emailChange);
            return "Email Has Changed Successfully";
        }

        public async Task<(string, RefreshToken)> RefreshToken(RefreshTokenRequest request)
        {
            var (accessToken, refreshToken) = await authServices.RefreshTokenAsync(request.RefreshToken);

            return (accessToken, refreshToken);
        }

        public async Task<string> LogoutAsync(string userId)
        {
            await userRepository.LogoutAsync(userId);
            return "Logout Sucess";
        }

    }
}
