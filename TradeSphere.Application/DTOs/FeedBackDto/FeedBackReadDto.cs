namespace TradeSphere.Application.DTOs.FeedBackDto
{
    public class FeedBackReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ProductName { get; set; }
    }
}
