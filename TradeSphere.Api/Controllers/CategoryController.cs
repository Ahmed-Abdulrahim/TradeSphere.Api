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

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CategoryListDto>> GetById(int id)
        {
            var data = await categoryUseCase.GetById(id);
            return Ok(data);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<CategoryListDto>> GetByName(string name)
        {
            var category = await categoryUseCase.GetByName(name);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var delete = await categoryUseCase.DeleteCategory(id);
            return Ok(new ApiResponse(204, "Deleted Successfully"));
        }
    }
}
