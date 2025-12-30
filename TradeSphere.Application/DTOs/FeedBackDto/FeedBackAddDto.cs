namespace TradeSphere.Application.DTOs.FeedBackDto
{
    public class FeedBackAddDto
    {
        public string AppUserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
    }
}
