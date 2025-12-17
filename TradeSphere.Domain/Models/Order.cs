namespace TradeSphere.Domain.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int AppUserId { get; set; }

        // Navigation properties
        public AppUser AppUser { get; set; }
        public Payment Payment { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
