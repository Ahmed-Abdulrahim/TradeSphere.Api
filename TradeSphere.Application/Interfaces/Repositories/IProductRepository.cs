namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetById(int id);
        Task<Product> GetByName(string name);
        Task<Product> AddProduct(Product addPrdouct);
        Task<Product> UpdateProduct(int id, Product updateProduct);
        Task<bool> DeleteProduct(int id);
    }
}
