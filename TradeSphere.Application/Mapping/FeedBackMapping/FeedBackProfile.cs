using TradeSphere.Application.DTOs.FeedBackDto;

namespace TradeSphere.Application.Mapping.FeedBackMapping
{
    public class FeedBackProfile : Profile
    {
        public FeedBackProfile()
        {
            CreateMap<FeedBackAddDto, FeedBack>().ReverseMap();
            CreateMap<FeedBackUpdateDto, FeedBack>().ReverseMap();
            CreateMap<FeedBack, FeedBackReadDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FirstName + "" + src.AppUser.LastName))
            .ReverseMap();
        }
    }
}