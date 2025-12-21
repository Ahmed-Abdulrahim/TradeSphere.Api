namespace TradeSphere.Application.DTOs.OrderDto.CreateOrderDto
{
    public class CreateOrderDto
    {
        public int AppUserId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
        public CreatePaymentDto Payment { get; set; }
    }



}
