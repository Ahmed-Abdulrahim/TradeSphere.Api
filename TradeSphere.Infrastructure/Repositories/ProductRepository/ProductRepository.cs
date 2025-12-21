namespace TradeSphere.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository(IUnitOfWork unit) : IProductRepository
    {
        public async Task<Product?> AddProduct(Product addProduct)
        {

            await unit.Repository<Product>().AddAsync(addProduct);
            var rowAffected = await unit.CommitAsync();
            return addProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await unit.Repository<Product>().GetByIdSpec(new ProductSpecification(id));
            unit.Repository<Product>().Delete(product);
            return await unit.CommitAsync() > 0;
        }


        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await unit.Repository<Product>().GetAllWithSpec(new ProductSpecification());

            return products;
        }

        public async Task<Product?> GetById(int id)
        {
            var product = await unit.Repository<Product>().GetByIdSpec(new ProductSpecification(id));
            return product;
        }

        public Task<Product> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> UpdateProduct(int id, Product updateProduct)
        {
            throw new NotImplementedException();
        }
    }
}
