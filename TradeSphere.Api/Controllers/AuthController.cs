namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthUseCase authUseCase) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResultDto>> Login([FromBody] UserLoginDto user)
        {
            var result = await authUseCase.LoginUser(user);
            if (result is null) return BadRequest(new ApiResponse(400, _Message: "Invalid Email Or Password"));
            return Ok(result);
        }
        [HttpPost("register")]

        public async Task<ActionResult<UserResultDto>> Register([FromBody] UserRegisterDto user)
        {
            var result = await authUseCase.RegisterUser(user);
            return Ok(result);
        }

        [HttpGet("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await authUseCase.ConfirmEmail(userId, token);
            return Ok(new ApiResponse(200, result));
        }
        [AllowAnonymous]
        [HttpGet("confirmChangeEmail")]
        public async Task<IActionResult> ConfirmChangeEmail([FromQuery] string userId, [FromQuery] string newEmail, [FromQuery] string token)
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

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            await authUseCase.ForgetPassword(email);
            return NoContent();
        }

        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            var result = await authUseCase.resetPassword(resetPassword);
            if (string.IsNullOrEmpty(result))
                return BadRequest(new ApiResponse(400, "Try Again"));

            return Ok(new ApiResponse(200, result));
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var (accessToken, refreshToken) = await authUseCase.RefreshToken(request);

            return Ok(new UserResultDto
            {
                Token = accessToken,
                RefreshTokenExpiration = refreshToken.ExpireOn
            });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var result = await authUseCase.LogoutAsync(userId);
            return Ok(new ApiResponse(200, result));
        }
    }
}
