using TradeSphere.Application.DTOs.OrderItemDto;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(OrderUseCase orderUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderItemInfoDto>>> GetAll()
        {
            var orders = await orderUseCase.GetAllOrder();
            if (orders is null) return NotFound(new ApiResponse(404));
            return Ok(orders);
        }
    }
}
