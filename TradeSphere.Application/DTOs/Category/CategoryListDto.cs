namespace TradeSphere.Application.DTOs.Category
{
    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductListDto> Products { get; set; }
    }
}
