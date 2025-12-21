namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductInfoDto>> GetAllProducts();
        Task<ProductInfoDto> GetById(int id);
        Task<ProductInfoDto> GetByName(string name);
        Task<ProductInfoDto> AddProduct(ProductAddDto categoryAddDto);
        Task<ProductInfoDto> UpdateProduct(int id, ProductAddDto categoryAddDto);
        Task<bool> DeleteProduct(int id);
    }
}
