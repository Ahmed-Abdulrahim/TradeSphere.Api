namespace TradeSphere.Domain.Models
{
    public class ShoppingCart : BaseEntity
    {
        public int AppUserId { get; set; }

        // Navigation properties
        public AppUser AppUser { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
