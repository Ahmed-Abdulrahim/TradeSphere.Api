namespace TradeSphere.Domain.Models.IdentityUser
{
    public class AppRole : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}
