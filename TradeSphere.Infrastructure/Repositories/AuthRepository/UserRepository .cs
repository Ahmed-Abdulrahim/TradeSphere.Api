namespace TradeSphere.Infrastructure.Repositories.AuthRepository
{
    public class UserRepository(UserManager<AppUser> userManager, IConfiguration? configuration, IEmailService? emailService) : IUserRepository
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
                var error = String.Join(separator: ",", success.Errors.Select(e => e.Description));
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

        public async Task<bool> ChangePassword(AppUser user, string currentPassword, string newPassword)
        {
            if (user is null) throw new Exception("UserNotFound");
            var changePassword = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!changePassword.Succeeded)
            {
                throw new Exception("Operation Failed");
            }
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

        public async Task ForgetPasswordAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null) throw new Exception("User Not Found");
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encoderToken = UrlEncoder.Default.Encode(token);
            var resetUrl =
         $"{configuration["EmailSettings:AppUrl"]}/api/Account/ResetPassword" +
         $"?userId={user.Id}&token={encoderToken}";
            await emailService.SendEmailAsync(user.Email, "ResetPassword", EmailBody(resetUrl));

        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            if (resetPassword.NewPassword != resetPassword.ConfirmPassword) throw new Exception("Password Dont Match");
            var user = await userManager.FindByEmailAsync(resetPassword.Email);
            if (user is null) throw new Exception("User Not Found");
            var decodedToken = WebUtility.UrlDecode(resetPassword.Token);
            var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPassword.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            return true;

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
        public async Task<string> ChangeEmail(string userId, ConfirmChangeEmailRequest emailChange)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null) throw new Exception("User Not Found");

            var existingUser = await userManager.FindByEmailAsync(emailChange.NewEmail);
            if (existingUser != null && existingUser.Id != user.Id)
            {
                return "Email already in use";
            }
            var tokenBytes = Convert.FromBase64String(emailChange.Token);
            var decodedToken = Encoding.UTF8.GetString(tokenBytes);
            var changeEmail = await userManager.ChangeEmailAsync(user, emailChange.NewEmail, decodedToken);
            if (!changeEmail.Succeeded)
            {
                return "Operation Failed";
            }
            var setUsernameResult = await userManager.SetUserNameAsync(user, emailChange.NewEmail);
            if (!setUsernameResult.Succeeded)
            {
                var errors = string.Join(", ", setUsernameResult.Errors.Select(e => e.Description));
                return $"Failed to update username: {errors}";
            }
            user.EmailConfirmed = true;
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                return $"Failed to confirm email: {errors}";
            }
            return "Success";
        }

        ////LoGout
        //public async Task<bool> LogOut(string email) 
        //{
        //    var user = await userManager.out
        //}
    }
}
