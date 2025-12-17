namespace TradeSphere.Application.UseCases
{
    public class AuthUseCase(IAuthService authServices, IUserRepository userRepository)
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
                Token = await authServices.GenerateJwtToken(addUser)
            };
            return result;
        }
        public async Task<UserResultDto> LoginUser(UserLoginDto loginUser)
        {
            var findUser = await userRepository.FindByEmailAsync(loginUser.Email);
            if (findUser is null) throw new Exception("InValid Email Or Password");
            var checkPassword = await userRepository.CheckPasswordAsync(findUser, loginUser.Password);
            if (!checkPassword) throw new Exception("invalid Email or Password");
            return new UserResultDto()
            {
                Email = loginUser.Email,
                UserName = findUser.UserName,
                Token = await authServices.GenerateJwtToken(findUser)
            };
        }
    }
}
