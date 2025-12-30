namespace TradeSphere.Application.UseCases
{
    public class ProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        public async Task<ProductInfoDto?> AddProduct(ProductAddDto productAdd)
        {

            var product = mapper.Map<Product>(productAdd);
            var addProduct = await productRepository.AddProduct(product);
            var productInfo = await productRepository.GetById(product.Id);
            if (productInfo is null) return null;
            var result = mapper.Map<ProductInfoDto>(productInfo);
            return result;

        }
        public async Task<bool> DeleteProduct(int id) => await productRepository.DeleteProduct(id);
        public async Task<List<ProductInfoDto>?> GetAllProduct()
        {
            var products = await productRepository.GetAllProducts();
            var mapProducts = mapper.Map<List<ProductInfoDto>>(products);
            return mapProducts;
        }
        public async Task<ProductInfoDto?> GetProductById(int id)
        {
            var product = await productRepository.GetById(id);
            return product is null ? null : mapper.Map<ProductInfoDto>(product);
        }

        public async Task<ProductInfoDto?> GetProductByName(string name)
        {
            var product = await productRepository.GetByName(name);
            if (product is null) return null;
            return mapper.Map<ProductInfoDto>(product);
        }

        public async Task<ProductInfoDto?> UpdateProduct(int id, ProductAddDto updateProductDto)
        {
            var getProductInfo = await productRepository.GetById(id);
            mapper.Map(updateProductDto, getProductInfo);
            var updateproduct = await productRepository.UpdateProduct(getProductInfo);
            if (updateproduct is null) return null;
            var productInfo = mapper.Map<ProductInfoDto>(updateproduct);
            return productInfo;
        }


    }
}
