using TradeSphere.Application.DTOs.Category;

namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryListDto>> GetAllCategory();
        Task<CategoryListDto> GetById(int id);
        Task<CategoryListDto> GetByName(string name);
        Task<CategoryListDto> AddCategory(CategoryAddDto categoryAddDto);
        Task<CategoryListDto> UpdateCategory(int id, CategoryAddDto categoryAddDto);
        Task<bool> DeleteCategory(int id);

    }
}
