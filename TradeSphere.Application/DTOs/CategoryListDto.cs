namespace TradeSphere.Application.DTOs
{
    public class CategoryListDto
    {
        public string Name { get; set; }
        public List<ProductListDto> Products { get; set; }
    }
}
