using TradeSphere.Application.DTOs.ShoppingCartDto;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController(ShoppingCartUseCase shoppingCartUseCase) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartByUserId()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            if (userName is null)
                return BadRequest(new ApiResponse(400, "Invalid UserName"));
            var shoppingCart = await shoppingCartUseCase.GetShoppingCartByUserIdAsync(userName);
            if (shoppingCart is null)
                return NotFound(new ApiResponse(404, "Shopping Cart not found"));
            return Ok(shoppingCart);
        }



    }
}
