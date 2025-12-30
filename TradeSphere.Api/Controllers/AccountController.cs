using static TradeSphere.Application.UseCases.AuthUseCase;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(AuthUseCase authUseCase) : ControllerBase
    {
        [HttpGet("GetProfile")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userEmail is null)
                return Unauthorized();
            var UserId = int.Parse(userEmail);
            var result = await authUseCase.GetProfile(UserId);
            if (result is null)
                return BadRequest(new ApiResponse(400, "Try Again"));
            return Ok(result);
        }

        [HttpPut("updateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfile)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userEmail is null)
                return Unauthorized();
            var UserId = int.Parse(userEmail);
            var result = await authUseCase.UpdateProfile(UserId, updateProfile);
            if (result is null)
                return BadRequest(new ApiResponse(400, "Try Again"));
            return Ok(result);
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null)
                return Unauthorized();

            var result = await authUseCase.ChangePassword(userEmail, currentPassword, newPassword);
            if (string.IsNullOrEmpty(result))
                return BadRequest(new ApiResponse(400, "Try Again"));

            return Ok(new ApiResponse(200, result));
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail(string newEmail)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await authUseCase.RequestChangeEmail(userEmail, newEmail);
            if (string.IsNullOrEmpty(result))
                return BadRequest(new ApiResponse(400, "InvalidOperation"));

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

    }
}
