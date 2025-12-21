namespace TradeSphere.Application.DTOs.OrderItemDto
{
    public class OrderItemInfoDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }


    }
}
