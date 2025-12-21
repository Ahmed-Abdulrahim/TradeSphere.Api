namespace TradeSphere.Application.UseCases
{
    public class ProductUseCase(IProductRepository productRepository, ILogger<ProductUseCase> logger)
    {
        public async Task<ProductInfoDto> AddProduct(ProductAddDto productAdd)
        {
            var addProduct = await productRepository.AddProduct(productAdd);
            if (addProduct is null)
            {
                logger.LogError("Add Product Is Nullable in Produt useCase");
                return null;
            }
            return addProduct;
        }

        public Task<bool> DeleteProduct(int id) => productRepository.DeleteProduct(id);

        public async Task<List<ProductInfoDto>> GetAllProduct() => await productRepository.GetAllProducts();

    }
}
