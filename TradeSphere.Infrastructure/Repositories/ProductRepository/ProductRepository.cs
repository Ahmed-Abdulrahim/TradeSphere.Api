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

        public async Task<Product> GetByName(string name)
        {
            var product = await unit.Repository<Product>().GetByIdSpec(new ProductSpecification(p => p.Name == name));
            return product;
        }

        public async Task<Product?> UpdateProduct(Product updateProduct)
        {
            unit.Repository<Product>().Update(updateProduct);

            return await unit.CommitAsync() > 0 ? updateProduct : null;
        }
        ///////////  
        public async Task<Product> GetByIdTracked(int id)
        {
            var product = await unit.Repository<Product>().GetByIdSpecTracked(new ProductSpecification(id));
            return product;
        }
    }
}
