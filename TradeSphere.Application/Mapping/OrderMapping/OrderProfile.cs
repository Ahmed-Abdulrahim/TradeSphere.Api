using TradeSphere.Application.DTOs.OrderDto;
using TradeSphere.Application.DTOs.OrderItemDto;
using TradeSphere.Application.DTOs.PaymentDto;
namespace TradeSphere.Application.Mapping.OrderMapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderInfoDto>()
                .ForMember(des => des.userName, m => m.MapFrom(src => src.AppUser.FirstName + " " + src.AppUser.LastName))
                .IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();

            CreateMap<Payment, PaymenInfoDto>()
                .ForMember(des => des.UserName, m => m.MapFrom(src => src.AppUser.FirstName + " " + src.AppUser.LastName))
                .ForMember(des => des.Status, o => o.MapFrom(src => src.Status.ToString()))
                .IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();

            CreateMap<OrderItem, OrderItemInfoDto>()
                .ForMember(des => des.ProductName, o => o.MapFrom(src => src.Product.Name))
                .IgnoreAllPropertiesWithAnInaccessibleSetter().ReverseMap();
        }
    }
}
