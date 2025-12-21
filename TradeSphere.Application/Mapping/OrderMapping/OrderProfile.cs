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

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Pending))
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());

            CreateMap<CreateOrderItemDto, OrderItem>();

            CreateMap<CreatePaymentDto, Payment>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PaymentStatus.Pending))
                .ForMember(dest => dest.Amount, opt => opt.Ignore());
        }
    }
}

