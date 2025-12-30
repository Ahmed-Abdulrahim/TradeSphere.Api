namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(AccountUseCase accountUseCase) : ControllerBase
    {
        [HttpGet("GetProfile")]
        [Authorize]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();

            var result = await accountUseCase.GetProfile(userId);
            if (result is null)
                return BadRequest(new ApiResponse(400, "Try Again"));
            return Ok(result);
        }

        [HttpPut("updateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto updateProfile)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized();
            var result = await accountUseCase.UpdateProfile(userId, updateProfile);
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

            var result = await accountUseCase.ChangePassword(userEmail, currentPassword, newPassword);
            if (string.IsNullOrEmpty(result))
                return BadRequest(new ApiResponse(400, "Try Again"));

            return Ok(new ApiResponse(200, result));
        }

        [HttpPost("changeEmail")]
        public async Task<IActionResult> ChangeEmail(string newEmail)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var result = await accountUseCase.RequestChangeEmail(userEmail, newEmail);
            if (string.IsNullOrEmpty(result))
                return BadRequest(new ApiResponse(400, "InvalidOperation"));

            return Ok(new ApiResponse(200, result));
        }



    }
}
