using AutoMapper;
using TradeSphere.Application.DTOs.Category;
using TradeSphere.Application.Interfaces.UnitOfWork;

namespace TradeSphere.Application.UseCases
{
    public class CategoryUseCase(ICategoryRepository categoryRepository)
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
            var category = await categoryRepository.GetByName(name);
            return category;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var deleteCategory = await categoryRepository.DeleteCategory(id);
            return deleteCategory;
        }

        public async Task<CategoryListDto> AddCategory(CategoryAddDto categoryAddDto)
        {
            var addCategory = await categoryRepository.AddCategory(categoryAddDto);
            return addCategory;
        }
        public async Task<CategoryListDto> UpdateCategory(int id, CategoryAddDto categoryAddDto)
        {
            var updatedCategory = await categoryRepository.UpdateCategory(id, categoryAddDto);
            return updatedCategory;
        }
    }
}
