namespace TradeSphere.Domain.Models
{
    public class CartItem : BaseEntity
    {
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}
