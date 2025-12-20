namespace TradeSphere.Application.DTOs.Category
{
    public class CategoryListDto
    {
        public string Name { get; set; }
        public List<ProductListDto> Products { get; set; }
    }
}
