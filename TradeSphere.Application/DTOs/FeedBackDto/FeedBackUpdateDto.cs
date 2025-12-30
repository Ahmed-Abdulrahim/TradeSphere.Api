namespace TradeSphere.Application.DTOs.FeedBackDto
{
    public class FeedBackUpdateDto
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
    }
}
