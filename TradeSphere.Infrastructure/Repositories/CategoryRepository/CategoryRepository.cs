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

        public async Task<CategoryListDto> GetById(int id)
        {
            var spec = new CategorySpecification(id);
            var category = await unit.Repository<Category>().GetByIdSpec(spec);
            var data = mapper.Map<CategoryListDto>(category);
            return data;
        }

        public async Task<CategoryListDto> GetByName(string name)
        {
            var spec = new CategorySpecification(c => c.Name == name);
            var category = await unit.Repository<Category>().GetByIdSpec(spec);
            var data = mapper.Map<CategoryListDto>(category);
            return data;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await unit.Repository<Category>().GetByIdAsync(id);
            unit.Repository<Category>().Delete(category);
            var rowAffected = await unit.CommitAsync();
            return rowAffected > 0 ? true : false;
        }

    }
}
