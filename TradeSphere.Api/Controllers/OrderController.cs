using TradeSphere.Application.DTOs.OrderDto;
using TradeSphere.Application.DTOs.OrderDto.CreateOrderDto;
using TradeSphere.Application.DTOs.OrderItemDto;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(OrderUseCase orderUseCase) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OrderInfoDto>>> GetAll()
        {
            var orders = await orderUseCase.GetAllOrder();
            if (orders is null) return NotFound(new ApiResponse(404));
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderInfoDto>> GetById(int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse(400, "Invalid Id"));
            var order = await orderUseCase.GetById(id);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(order);
        }

        [HttpGet("Users/{userId}")]
        public async Task<ActionResult<List<OrderInfoDto>>> GetByUserId(int userId)
        {
            var order = await orderUseCase.GetByUserId(userId);
            if (order is null) return NotFound(new ApiResponse(404));
            return Ok(order);
        }


        [HttpPost]
        public async Task<ActionResult<OrderInfoDto>> AddOrder([FromBody] CreateOrderDto createOrderDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid data"));

            if (createOrderDto.AppUserId <= 0)
                return BadRequest(new ApiResponse(400, "Invalid User Id"));

            if (createOrderDto.OrderItems == null || !createOrderDto.OrderItems.Any())
                return BadRequest(new ApiResponse(400, "Order must contain at least one item"));

            var order = await orderUseCase.AddOrder(createOrderDto);

            if (order == null)
                return BadRequest(new ApiResponse(400, "Failed to create order"));

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);


        }
    }
}
