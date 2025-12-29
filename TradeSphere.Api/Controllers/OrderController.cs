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


        [HttpPost("Checkout")]
        public async Task<ActionResult<OrderInfoDto>> Checkout([FromBody] CreateOrderDto createOrderDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid data"));

            if (createOrderDto.AppUserId <= 0)
                return BadRequest(new ApiResponse(400, "Invalid User Id"));

            if (createOrderDto.OrderItems == null || !createOrderDto.OrderItems.Any())
                return BadRequest(new ApiResponse(400, "Order must contain at least one item"));

            var order = await orderUseCase.Checkout(createOrderDto);

            if (order == null)
                return BadRequest(new ApiResponse(400, "Failed to create order"));

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);


        }


        [HttpPost("cancel/{id}")]
        public async Task<ActionResult> CancelOrder(int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse(400, "Invalid Id"));
            var result = await orderUseCase.CancelOrder(id);
            if (result is null)
                return BadRequest(new ApiResponse(400, "Failed to cancel order"));
            return NoContent();
        }

        [HttpPut("status")]
        public async Task<ActionResult> UpdateOrderStatus([FromBody] UpdateOrderStatusDto updateOrderStatusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse(400, "Invalid data"));
            var result = await orderUseCase.UpdateOrderStatus(updateOrderStatusDto);
            if (result is null)
                return BadRequest(new ApiResponse(400, "Failed to update order status"));
            return NoContent();
        }

        [HttpGet("status/{Id}")]
        public async Task<ActionResult<ApiResponse>> GetOrderStatuses(int orderId)
        {
            var statuse = await orderUseCase.GetStatus(orderId);
            return Ok(new ApiResponse(200, statuse));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse(400, "Invalid Id"));
            var result = await orderUseCase.DeleteOrder(id);
            if (result is null)
                return BadRequest(new ApiResponse(400, "Failed to delete order"));
            return NoContent();
        }
    }
}