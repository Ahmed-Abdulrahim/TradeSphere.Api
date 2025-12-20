namespace TradeSphere.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }


        // Navigation properties
        public Category Category { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<FeedBack> FeedBacks { get; set; }

    }
}
