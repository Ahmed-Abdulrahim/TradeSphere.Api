namespace TradeSphere.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Admin")]
    public class RoleController(RoleUseCase roleUseCase) : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<RoleDto>> GetAllRoles()
        {
            var roles = await roleUseCase.GetAllRoles();
            if (roles is null) return NotFound("No roles found.");
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await roleUseCase.GetRoleById(id);
            if (role is null) return NotFound($"Role with ID {id} not found.");
            return Ok(role);
        }

        [HttpGet("userId")]

        public async Task<ActionResult<RoleDto>> GetUserRole(int userId)
        {
            var role = await roleUseCase.GetUserRole(userId);
            if (role is null) return NotFound($"Role for User ID {userId} not found.");
            return Ok(role);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRole(int roleId, string name)
        {
            var result = await roleUseCase.UpdateRole(roleId, name);
            if (!result) return NotFound($"Role with ID {roleId} not found.");
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(string roleName)
        {
            var result = await roleUseCase.AddRole(roleName);
            if (result == "Role name cannot be empty")
                return BadRequest(new ApiResponse(400, result));
            return Ok(new ApiResponse(200, result));
        }
        [HttpPost("ChangeUserRole")]
        public async Task<ActionResult> ChangeUserRole(int userId, string roleName)
        {
            var result = await roleUseCase.ChangeUserRole(userId, roleName);
            if (!result) return NotFound($"Failed to change role for User ID {userId}.");
            return NoContent();
        }

    }
}
