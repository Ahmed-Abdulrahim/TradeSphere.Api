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
            var userClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userClaim is null)
                return BadRequest(new ApiResponse(400, "Invalid UserName"));
            var userId = int.Parse(userClaim);
            var shoppingCart = await shoppingCartUseCase.GetShoppingCartByUserIdAsync(userId);
            if (shoppingCart is null)
                return NotFound(new ApiResponse(404, "Shopping Cart not found"));
            return Ok(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> AddToCart([FromBody] AddToCartDto dto)
        {
            var userClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userClaim is null)
                return BadRequest(new ApiResponse(400, "Invalid UserName"));
            var userId = int.Parse(userClaim);
            var updatedCart = await shoppingCartUseCase.AddToCartAsync(userId, dto);
            return Ok(updatedCart);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> RemoveFromCart([FromQuery] int productId, [FromQuery] int quantity)
        {
            var userClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userClaim is null)
                return BadRequest(new ApiResponse(400, "Invalid UserName"));
            var userId = int.Parse(userClaim);
            var updatedCart = await shoppingCartUseCase.RemoveFromCartAsync(userId, productId, quantity);
            if (updatedCart is null)
                return NotFound(new ApiResponse(404, "Shopping Cart not found"));
            return Ok(updatedCart);
        }
        [HttpPost("clear")]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> ClearCart()
        {
            var userClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userClaim is null)
                return BadRequest(new ApiResponse(400, "Invalid UserName"));
            var userId = int.Parse(userClaim);
            var clearedCart = await shoppingCartUseCase.ClearCartAsync(userId);
            if (clearedCart is null)
                return NotFound(new ApiResponse(404, "Shopping Cart not found"));
            return Ok(clearedCart);
        }

        [HttpPut("{productId}")]
        [Authorize]
        public async Task<ActionResult<ShoppingCartDto>> UpdateCartItemQuantity(int productId, int quantity)
        {
            var userClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userClaim is null)
                return BadRequest(new ApiResponse(400, "Invalid UserName"));
            var userId = int.Parse(userClaim);
            var updatedCart = await shoppingCartUseCase.UpdateCart(userId, productId, quantity);
            if (updatedCart is null)
                return NotFound(new ApiResponse(404, "Shopping Cart not found"));
            return Ok(updatedCart);
        }
    }
}
