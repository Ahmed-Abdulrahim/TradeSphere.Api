namespace TradeSphere.Application.Mapping.RoleMapping
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<AppRole, RoleDto>();
        }
    }
}
