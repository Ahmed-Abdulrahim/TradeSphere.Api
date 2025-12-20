using AutoMapper;
using TradeSphere.Application.DTOs;
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
    }
}
