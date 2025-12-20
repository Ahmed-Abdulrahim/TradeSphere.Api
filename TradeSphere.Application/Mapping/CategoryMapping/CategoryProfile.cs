using AutoMapper;

using TradeSphere.Application.DTOs;

namespace TradeSphere.Application.Mapping.CategoryProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
