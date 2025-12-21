namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductController(ProductUseCase productUseCase, ILogger<ProductController> logger) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<ProductInfoDto>>> GetAllProduct()
        {
            var products = await productUseCase.GetAllProduct();
            if (products is null) return NotFound();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInfoDto>> GetProductById(int id)
        {
            if (id <= 0) return BadRequest(new ApiResponse(400, "InvalidId"));
            var product = await productUseCase.GetProductById(id);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(product);
        }

        [HttpGet("GetByName{name}")]
        public async Task<ActionResult<ProductInfoDto>> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest(new ApiResponse(400, "Invalid Name"));
            var product = await productUseCase.GetProductByName(name);
            if (product is null) return NotFound(new ApiResponse(404));
            return Ok(product);

        }

        [HttpPost]
        public async Task<ActionResult<ProductInfoDto>> AddProduct(ProductAddDto productAdd)
        {
            if (productAdd is null)
            {
                logger.LogWarning("Model Of ProductAdd Dto Is null ");
                return BadRequest(new ApiResponse(400, _Message: "Invalid Data"));
            }

            var addProduct = await productUseCase.AddProduct(productAdd);
            if (addProduct is null)
            {
                logger.LogError($"Cant Add Product With Name {productAdd.Name}");
                return BadRequest(new ApiResponse(400));
            }
            return Ok(addProduct);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductInfoDto>> UpdateProduct(int id, ProductAddDto updateProduct)
        {
            if (id <= 0 || updateProduct is null)
                return BadRequest(new ApiResponse(400, _Message: "Invalid Data"));
            var product = await productUseCase.UpdateProduct(id, updateProduct);
            if (product is null)
                return NotFound(new ApiResponse(404, _Message: "Product not found or cant update"));
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest(new ApiResponse(400, "Invalid product id"));

            var deleted = await productUseCase.DeleteProduct(id);

            if (!deleted)
                return NotFound(new ApiResponse(404, "Product not found"));

            return NoContent();
        }

    }
}
