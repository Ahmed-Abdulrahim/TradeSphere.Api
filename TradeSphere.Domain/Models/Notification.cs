using TradeSphere.Domain.Models;

namespace TradeSphere.Domain.Models
{
    public class Notification : BaseEntity
    {
        public int AppUserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public AppUser AppUser { get; set; }

    }
}
