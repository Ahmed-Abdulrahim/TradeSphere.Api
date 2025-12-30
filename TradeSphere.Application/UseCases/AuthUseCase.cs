namespace TradeSphere.Application.UseCases
{
    public class AuthUseCase(IAuthService authServices, IAuthRepository authRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        public async Task<UserResultDto> RegisterUser(UserRegisterDto registerUser)
        {
            var existingUser = await authRepository.FindByEmailAsync(registerUser.Email);
            if (existingUser != null)
            {
                throw new Exception("This Email Is Already Exist");
            }

            if (await authRepository.CheckIfUserNameExistAsync(registerUser.UserName))
            {
                throw new Exception("This UserName Is Already Exist");
            }
            var addUser = new AppUser()
            {
                UserName = registerUser.UserName,
                Email = registerUser.Email,
                PhoneNumber = registerUser.PhoneNumber,
            };
            await authRepository.CreateAsync(addUser, registerUser.Password);
            await authRepository.AddDefaultRole(addUser, "User");
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
            var findUser = await authRepository.FindByEmailAsync(loginUser.Email);
            if (findUser is null) throw new Exception("InValid Email Or Password");
            var checkPassword = await authRepository.CheckPasswordAsync(findUser, loginUser.Password);
            if (!checkPassword) throw new Exception("invalid Email or Password");
            if (!await authRepository.IsEmailConfirmedAsync(findUser))
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
            var user = await authRepository.FindByUserIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            var result = await authRepository.ConfirmEmailAsync(user, token);

            return "Email confirmed successfully";
        }


        public async Task<string> ForgetPassword(string email)
        {
            await authRepository.ForgetPasswordAsync(email);
            return "Email Has Sent Success";

        }

        public async Task<string> resetPassword(ResetPasswordDto resetPassword)
        {
            var flag = await authRepository.ResetPasswordAsync(resetPassword);
            if (!flag) return null;
            return "Password Changed Success";

        }

        public async Task<string> ConfrimEmailForAfterChanging(string userId, ConfirmChangeEmailRequest emailChange)
        {
            await authRepository.ChangeEmailAsync(userId, emailChange);
            return "Email Has Changed Successfully";
        }

        public async Task<(string, RefreshToken)> RefreshToken(RefreshTokenRequest request)
        {
            var (accessToken, refreshToken) = await authServices.RefreshTokenAsync(request.RefreshToken);

            return (accessToken, refreshToken);
        }

        public async Task<string> LogoutAsync(string userId)
        {
            await authRepository.LogoutAsync(userId);
            return "Logout Sucess";
        }



    }
}
