namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(CategoryUseCase categoryUseCase, ILogger<CategoryController> logger) : ControllerBase
    {
        [HttpGet("GetAllCategory")]
        public async Task<ActionResult<List<CategoryListDto>>> GetAll()
        {
            var categories = await categoryUseCase.GetAllCategory();
            if (categories == null || !categories.Any())
                return NotFound(new ApiResponse(404, "No categories found"));
            return Ok(categories);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CategoryListDto>> GetById(int id)
        {
            var data = await categoryUseCase.GetById(id);
            if (data == null) return NotFound(new ApiResponse(404));
            return Ok(data);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<CategoryListDto>> GetByName(string name)
        {
            try
            {
                var category = await categoryUseCase.GetByName(name);
                if (category == null)
                    return NotFound(new ApiResponse(404, $"Category with name {name} not found"));

                return Ok(category);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in GetByName controller");
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var deleted = await categoryUseCase.DeleteCategory(id);
                if (!deleted)
                    return NotFound(new ApiResponse(404, $"Category with Id {id} not found"));

                return Ok(new ApiResponse(204, "Deleted Successfully"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in DeleteCategory controller");
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }

        //Add && Update
        [HttpPost]
        public async Task<ActionResult<CategoryListDto>> AddCategory(CategoryAddDto categoryAddDto)
        {
            try
            {
                var addCategory = await categoryUseCase.AddCategory(categoryAddDto);
                if (addCategory == null)
                    return BadRequest(new ApiResponse(400, $"Cant Add Category With name {categoryAddDto.Name}"));
                return Ok(addCategory);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in AddCategory controller");
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }

        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryListDto>> UpdateCategory(int id, CategoryAddDto categoryAddDto)
        {
            if (categoryAddDto == null)
                return BadRequest(new ApiResponse(400, "Category data is required"));

            try
            {
                var updateCategory = await categoryUseCase.UpdateCategory(id, categoryAddDto);
                if (updateCategory == null)
                    return BadRequest(new ApiResponse(400, $"Data In Valid"));

                return Ok(updateCategory);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception in UpdateCategory controller");
                return StatusCode(500, new ApiResponse(500, "Internal server error"));
            }
        }


    }
}
