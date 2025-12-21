namespace TradeSphere.Application.Mapping.ProductMapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductAddDto>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Product, ProductInfoDto>().ForMember(des => des.CategoryName, src => src.MapFrom(s => s.Category.Name))
                .ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
