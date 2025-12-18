using Microsoft.AspNetCore.Identity;

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
            return Ok(result);
        }



    }
}
