namespace TradeSphere.Domain.Models
{
    public class FeedBack : BaseEntity
    {
        public int AppUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }

        // Navigation property
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }

    }
}
