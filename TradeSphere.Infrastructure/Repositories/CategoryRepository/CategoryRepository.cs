namespace TradeSphere.Infrastructure.Repositories.CategoryRepository
{
    public class CategoryRepository(IUnitOfWork unit, IMapper mapper, ILogger<CategoryRepository> logger) : ICategoryRepository
    {

        public async Task<List<CategoryListDto>> GetAllCategory()
        {
            var spec = new CategorySpecification();
            var categories = await unit.Repository<Category>().GetAllWithSpec(spec);
            if (!categories.Any()) return new List<CategoryListDto>();
            var data = mapper.Map<List<CategoryListDto>>(categories);
            return data;
        }

        public async Task<CategoryListDto> GetById(int id)
        {
            var spec = new CategorySpecification(id);
            var category = await unit.Repository<Category>().GetByIdSpec(spec);
            if (category is null)
            {
                logger.LogWarning("Category with id {Id} not found.", id);
                return null;
            }
            var data = mapper.Map<CategoryListDto>(category);
            return data;
        }

        public async Task<CategoryListDto> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                logger.LogWarning("GetByName called with null or empty name");
                return null;
            }

            try
            {
                var spec = new CategorySpecification(c => c.Name == name);
                var category = await unit.Repository<Category>().GetByIdSpec(spec);

                if (category is null)
                {
                    logger.LogWarning("No Category Found With Name {Name}", name);
                    return null;
                }

                return mapper.Map<CategoryListDto>(category);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while fetching category with name {Name}", name);
                throw;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            if (id == 0)
            {
                logger.LogInformation("This Id Is Invalid");
                return false;
            }
            try
            {
                var category = await unit.Repository<Category>().GetByIdAsync(id);
                if (category is null)
                {
                    logger.LogWarning($"There is No Category with this Id ${id}");
                    return false;
                }
                unit.Repository<Category>().Delete(category);
                var rowAffected = await unit.CommitAsync();
                return rowAffected > 0 ? true : false;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deleting category with Id {Id}", id);
                throw;
            }

        }

        public async Task<CategoryListDto> AddCategory(CategoryAddDto categoryAddDto)
        {
            if (categoryAddDto == null)
            {
                logger.LogWarning("AddCategory called with null CategoryAddDto");
                return null;
            }

            if (String.IsNullOrWhiteSpace(categoryAddDto.Name))
            {
                logger.LogInformation("Enter Valid Name ");
                return null;
            }
            try
            {
                var category = mapper.Map<Category>(categoryAddDto);
                await unit.Repository<Category>().AddAsync(category);

                var rowAffected = await unit.CommitAsync();
                var categoryList = mapper.Map<CategoryListDto>(category);
                return categoryList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Some Thing Went Wrong With AddCategory");
                throw;
            }
        }

        public async Task<CategoryListDto> UpdateCategory(int id, CategoryAddDto categoryAddDto)
        {
            if (id == 0 || categoryAddDto == null || String.IsNullOrWhiteSpace(categoryAddDto.Name))
            {
                logger.LogWarning("UpdateCategory called with null CategoryAddDto Or Id");
                return null;
            }
            try
            {
                var spec = new CategorySpecification(id);
                var category = await unit.Repository<Category>().GetByIdSpec(spec);
                if (category is null)
                {
                    logger.LogInformation($"Catgoey With Id {id}");
                    return null;
                }
                category.Name = categoryAddDto.Name;
                unit.Repository<Category>().Update(category);
                var rowAffected = await unit.CommitAsync();
                var categoryList = mapper.Map<CategoryListDto>(category);
                return categoryList;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Some Thing Went Wrong With UpdateCategory");
                throw;
            }

        }
    }
}
