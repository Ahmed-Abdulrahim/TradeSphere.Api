using TradeSphere.Application.DTOs;

namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryListDto>> GetAllCategory();

    }
}
