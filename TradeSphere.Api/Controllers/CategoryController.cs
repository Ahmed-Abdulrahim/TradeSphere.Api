using TradeSphere.Application.DTOs;

namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(CategoryUseCase categoryUseCase) : ControllerBase
    {
        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<CategoryListDto>> GetAll()
        {
            var categories = await categoryUseCase.GetAllCategory();
            return Ok(categories);
        }
    }
}
