namespace TradeSphere.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository(IUnitOfWork unit, IMapper mapper, ILogger<ProductRepository> logger) : IProductRepository
    {
        public async Task<ProductInfoDto?> AddProduct(ProductAddDto categoryAddDto)
        {
            if (categoryAddDto == null)
            {
                logger.LogWarning("Cant Instert Anullable Data");
                return null;
            }
            try
            {
                var product = mapper.Map<Product>(categoryAddDto);
                await unit.Repository<Product>().AddAsync(product);
                var rowAffected = await unit.CommitAsync();
                var productSpec = new ProductSpecification(product.Id);
                var getProduct = await unit.Repository<Product>().GetByIdSpec(productSpec);
                var categoryInfo = mapper.Map<ProductInfoDto>(getProduct);
                return rowAffected > 0 ? categoryInfo : null;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something Went Wrong With AddProduct Repository");
                throw;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await unit.Repository<Product>().GetByIdSpec(new ProductSpecification(id));

            if (product == null)
                return false;

            unit.Repository<Product>().Delete(product);
            return await unit.CommitAsync() > 0;
        }


        public async Task<List<ProductInfoDto>> GetAllProducts()
        {
            var products = await unit.Repository<Product>().GetAllWithSpec(new ProductSpecification());

            return mapper.Map<List<ProductInfoDto>>(products);
        }


        public Task<ProductInfoDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductInfoDto> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ProductInfoDto> UpdateProduct(int id, ProductAddDto categoryAddDto)
        {
            throw new NotImplementedException();
        }
    }
}
