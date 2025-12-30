namespace TradeSphere.Infrastructure.Repositories.AuthRepository
{
    public class AccountRepository(UserManager<AppUser> userManager, IConfiguration? configuration, IEmailService? emailService) : IAccountRepository
    {
        public async Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            if (user is null) throw new Exception("UserNotFound");
            var changePassword = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!changePassword.Succeeded)
            {
                throw new Exception("Operation Failed");
            }
            return true;
        }
        public async Task<AppUser> GetProfileAsync(string userId)
        {
            var findUser = await userManager.FindByIdAsync(userId.ToString());
            if (findUser is null) return null!;
            return findUser;
        }

        public async Task RequestChangeEmailAsync(string currentEmail, string newEmail)
        {
            var user = await userManager.FindByEmailAsync(currentEmail);
            if (user is null) throw new Exception("UserNotFound");
            var token = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var base64Token = Convert.ToBase64String(tokenBytes);
            var resetUrl =
         $"{configuration["EmailSettings:AppUrl"]}/api/Account/ConfirmChangeEmail" +
         $"?userId={user.Id}&newEmail={newEmail}&token={base64Token}";
            await emailService.SendEmailAsync(newEmail, "Confirm Change Email", EmailBody(resetUrl));
        }

        public async Task<AppUser> UpdateProfileAsync(AppUser appUser)
        {
            if (appUser is null) throw new Exception("User Not Found");
            var updateUser = await userManager.UpdateAsync(appUser);
            return appUser;
        }


        //Private Method
        private string EmailBody(string confirmUrl)
        {

            var emailBody = $"""
<html>
  <body style="font-family: Arial, sans-serif; background-color: #f4f4f7; margin: 0; padding: 0;">
    <table width="100%" cellpadding="0" cellspacing="0">
      <tr>
        <td align="center">
          <table width="600" cellpadding="0" cellspacing="0" style="background-color: #ffffff; border-radius: 10px; padding: 30px; margin-top: 50px;">
            <tr>
              <td style="text-align: center;">
                <h1 style="color: #2c3e50;">Welcome to TradeSphere!</h1>
                <p style="color: #555555; font-size: 16px;">
                  We're excited to have you on board. Please confirm your email to start exploring our platform.
                </p>
                <a href="{confirmUrl}" style="display: inline-block; padding: 15px 25px; font-size: 16px; color: #ffffff; background-color: #ff6f61; text-decoration: none; border-radius: 5px; margin-top: 20px;">
                  Confirm Email
                </a>
                <p style="color: #888888; font-size: 12px; margin-top: 30px;">
                  If you didn't sign up for TradeSphere, you can ignore this email.
                </p>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
""";

            return emailBody;
        }
    }
}
