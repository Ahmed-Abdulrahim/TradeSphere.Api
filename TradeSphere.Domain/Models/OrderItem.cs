
namespace TradeSphere.Domain.Models
{
    public class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
