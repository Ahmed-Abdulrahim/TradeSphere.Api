using TradeSphere.Application.DTOs.RolesDto;

namespace TradeSphere.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<List<AppRole>> GetAllRoles();
        public Task<AppRole> GetRoleById(int roleId);
        public Task<AppRole> GetUserRole(int UserId);
        public Task<bool> UpdateRole(int roleId, string name);
        public Task<string> AddRole(string roleName);
        public Task<bool> ChangeUserRole(int userId, string roleName);
    }
}
