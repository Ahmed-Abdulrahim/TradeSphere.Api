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
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductInfoDto>> AddProduct(ProductAddDto productAdd)
        {
            if (productAdd is null)
            {
                logger.LogWarning("Model Of ProductAdd Dto Is null ");
                return BadRequest(new ApiResponse(404, "Invalid Data"));
            }
            try
            {
                var addProduct = await productUseCase.AddProduct(productAdd);
                if (addProduct is null)
                {
                    logger.LogInformation($"Cant Add Product With Name {productAdd.Name}");
                    return BadRequest(new ApiResponse(400));
                }
                return Ok(addProduct);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in AddCategory controller");
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
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
