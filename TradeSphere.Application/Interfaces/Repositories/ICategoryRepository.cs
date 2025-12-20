using TradeSphere.Application.DTOs;

namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryListDto>> GetAllCategory();
        Task<CategoryListDto> GetById(int id);
        Task<CategoryListDto> GetByName(string name);
        Task<bool> DeleteCategory(int id);

    }
}
