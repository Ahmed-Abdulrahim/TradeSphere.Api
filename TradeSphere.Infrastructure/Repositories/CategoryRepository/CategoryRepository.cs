using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSphere.Application.DTOs;

namespace TradeSphere.Infrastructure.Repositories.CategoryRepository
{
    public class CategoryRepository(IUnitOfWork unit, IMapper mapper) : ICategoryRepository
    {
        public async Task<List<CategoryListDto>> GetAllCategory()
        {
            var spec = new CategorySpecification();
            var categories = await unit.Repository<Category>().GetAllWithSpec(spec);
            var data = mapper.Map<List<CategoryListDto>>(categories);
            return data;
        }
    }
}
