namespace TradeSphere.Application.UseCases
{
    public class CategoryUseCase(ICategoryRepository categoryRepository, ILogger<CategoryUseCase> logger)
    {
        public async Task<List<CategoryListDto>> GetAllCategory()
        {
            var categories = await categoryRepository.GetAllCategory();
            return categories;
        }

        public async Task<CategoryListDto> GetById(int id)
        {
            var category = await categoryRepository.GetById(id);
            return category;
        }

        public async Task<CategoryListDto> GetByName(string name)
        {
            try
            {
                var category = await categoryRepository.GetByName(name);
                if (category == null)
                    logger.LogInformation("Category with name {Name} not found in UseCase", name);

                return category;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in UseCase while getting category with name {Name}", name);
                throw;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var deleteCategory = await categoryRepository.DeleteCategory(id);
            if (!deleteCategory)
            {
                logger.LogInformation($"There is No Category With This Id {id}");
                return false;
            }
            return deleteCategory;
        }

        public async Task<CategoryListDto> AddCategory(CategoryAddDto categoryAddDto)
        {
            var addCategory = await categoryRepository.AddCategory(categoryAddDto);
            if (addCategory is null)
            {
                logger.LogInformation($"Cant Add Category");
                return null;
            }
            return addCategory;
        }
        public async Task<CategoryListDto> UpdateCategory(int id, CategoryAddDto categoryAddDto)
        {
            var updatedCategory = await categoryRepository.UpdateCategory(id, categoryAddDto);
            if (updatedCategory is null)
            {
                logger.LogInformation($"Cant updatedCategory");
                return null;
            }
            return updatedCategory;
        }
    }
}
