namespace TradeSphere.Domain.Models
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Cancelled
    }

    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public int AppUserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime PaymentDate { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public AppUser AppUser { get; set; }
    }

}
