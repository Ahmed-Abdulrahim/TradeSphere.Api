namespace TradeSphere.Application.DTOs.OrderDto.CreateOrderDto
{
    public class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
