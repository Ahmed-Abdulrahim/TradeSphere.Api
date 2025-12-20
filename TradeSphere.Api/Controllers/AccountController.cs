using static TradeSphere.Application.UseCases.AuthUseCase;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(AuthUseCase authUseCase) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResultDto>> Login([FromBody] UserLoginDto user)
        {
            var result = await authUseCase.LoginUser(user);
            if (result is null) return BadRequest(new ApiResponse(400, "Invalid Email Or Password"));
            return Ok(result);
        }
        [HttpPost("register")]

        public async Task<ActionResult<UserResultDto>> Register([FromBody] UserRegisterDto user)
        {
            var result = await authUseCase.RegisterUser(user);
            return Ok(result);
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await authUseCase.ConfirmEmail(userId, token);
            return Ok(new ApiResponse(200, result));
        }


        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var user = User.FindFirstValue(ClaimTypes.Email);
            if (user is null) return Unauthorized(value: "un Authorized");
            var changePassword = await authUseCase.ChangePassword(user, currentPassword, newPassword);
            if (string.IsNullOrEmpty(changePassword)) return BadRequest(new ApiResponse(400, "Try Again"));
            return Ok(new ApiResponse(200, changePassword));
        }

        [HttpPost("forgertPassword")]
        [Authorize]
        public async Task<IActionResult> ForgertPassword(string email)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email)!;
            if (userEmail != email) return BadRequest(new ApiResponse(400, "Invalid Email"));
            await authUseCase.ForgetPassword(email);
            return NoContent();

        }

        [HttpPost(template: "ResetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var result = await authUseCase.resetPassword(resetPassword);
            if (string.IsNullOrEmpty(result)) return BadRequest(new ApiResponse(400, "Try Again"));
            return Ok(new ApiResponse(200, result));

        }


        [HttpPost("ChangeEmail")]
        [Authorize]
        public async Task<IActionResult> ChangeEmail(string newEmail)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await authUseCase.RequestChangeEmail(userEmail, newEmail);
            if (string.IsNullOrEmpty(result)) return BadRequest(new ApiResponse(400, "InvalidOperation"));
            return Ok(new ApiResponse(200, result));
        }

        [HttpGet("ConfirmChangeEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string newEmail, [FromQuery] string token)
        {
            var result = await authUseCase.ConfrimEmailForAfterChanging(
                userId,
                new ConfirmChangeEmailRequest
                {
                    NewEmail = newEmail,
                    Token = token
                });

            if (string.IsNullOrEmpty(result))
                return BadRequest(new ApiResponse(400, "Invalid or expired token"));

            return Ok(new ApiResponse(200, result));
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var (accessToken, refreshToken) = await authUseCase.RefreshToken(request);

            return Ok(new UserResultDto
            {
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpireOn
            });
        }






    }
}
