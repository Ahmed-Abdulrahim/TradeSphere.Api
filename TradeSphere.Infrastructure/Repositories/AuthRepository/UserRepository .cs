using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace TradeSphere.Infrastructure.Repositories.AuthRepository
{
    public class UserRepository(UserManager<AppUser> userManager, IConfiguration configuration, IEmailService emailService) : IUserRepository
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
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token);
            var confirmUrl =
             $"{configuration["EmailSettings:AppUrl"]}/api/Account/confirmEmail" +
             $"?userId={user.Id}&token={encodedToken}";
            await emailService.SendEmailAsync(user.Email, "Confirm your email", EmailBody(confirmUrl));
            return user;
        }

        public async Task<AppUser> FindByUserIdAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return null;
            return user;
        }

        public async Task<bool> IsEmailConfirmed(AppUser user)
        {
            var flag = await userManager.IsEmailConfirmedAsync(user);
            if (!flag) throw new Exception("Email Must Be Confirmed");
            return true;
        }

        public async Task<bool> ConfirmEmailAync(AppUser user, string token)
        {
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                throw new Exception("Confirmation failed");
            return true;
        }

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
