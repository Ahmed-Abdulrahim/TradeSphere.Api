namespace TradeSphere.Application.Mapping.ShoppingCartMapping
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.CartItems.FirstOrDefault().ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.CartItems.FirstOrDefault().Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CartItems.FirstOrDefault().Product.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.CartItems.FirstOrDefault().Quantity))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.CartItems.FirstOrDefault().Quantity * src.CartItems.FirstOrDefault().Product.Price));
        }
    }
}
