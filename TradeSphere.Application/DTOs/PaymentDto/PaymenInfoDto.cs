namespace TradeSphere.Application.DTOs.PaymentDto
{
    public class PaymenInfoDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
