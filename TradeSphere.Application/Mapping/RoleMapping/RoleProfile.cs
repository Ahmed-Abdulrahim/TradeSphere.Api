using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSphere.Application.DTOs.RolesDto;

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
