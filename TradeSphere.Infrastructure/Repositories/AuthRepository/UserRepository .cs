namespace TradeSphere.Infrastructure.Repositories.AuthRepository
{
    public class UserRepository(UserManager<AppUser> userManager) : IUserRepository
    {
        public async Task<AppUser> FindByEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return null;
            return user;
        }

        public async Task<bool> CheckIfUserNameExist(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            var flag = await userManager.CheckPasswordAsync(user, password);
            return flag;
        }

        public async Task<AppUser> CreateAsync(AppUser user, string password)
        {
            var success = await userManager.CreateAsync(user, password);
            if (!success.Succeeded)
            {
                var error = String.Join(",", success.Errors.Select(e => e.Description));
                throw new Exception($"{error}");
            }
            return user;
        }
    }
}
