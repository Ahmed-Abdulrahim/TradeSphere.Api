
namespace TradeSphere.Domain.Models.IdentityUser
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation properties
        public ShoppingCart ShoppingCart { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<FeedBack> FeedBacks { get; set; }
        public List<Order> Orders { get; set; }
        public List<Payment> Payments { get; set; }
    }
}
