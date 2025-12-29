using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSphere.Application.DTOs.RolesDto;

namespace TradeSphere.Application.UseCases
{
    public class RoleUseCase(IMapper mapper, IRoleRepository roleRepository)
    {
        public async Task<List<RoleDto>> GetAllRoles()
        {
            var roles = await roleRepository.GetAllRoles();
            if (roles is null) return null;
            return mapper.Map<List<RoleDto>>(roles);
        }


        public async Task<RoleDto> GetRoleById(int roleId)
        {
            var role = await roleRepository.GetRoleById(roleId);
            if (role is null) return null;
            return mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> GetUserRole(int userId)
        {
            var role = await roleRepository.GetUserRole(userId);
            if (role is null) return null;
            return mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UpdateRole(int roleId, string name)
        {
            return await roleRepository.UpdateRole(roleId, name);
        }

        public async Task<string> AddRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return "Role name cannot be empty";
            return await roleRepository.AddRole(roleName);
        }


    }
}
