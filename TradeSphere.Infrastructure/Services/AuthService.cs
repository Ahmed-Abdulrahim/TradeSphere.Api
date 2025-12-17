using Microsoft.Extensions.Configuration;

namespace TradeSphere.Infrastructure.Services
{
    public class AuthService(UserManager<AppUser> userManager, IConfiguration configration) : IAuthService
    {

        public async Task<string> GenerateJwtToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.MobilePhone , user.PhoneNumber!)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var items in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, items));
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configration["JwtOptions:secretKey"]!));
            var token = new JwtSecurityToken
                (
                issuer: configration["JwtOptions:issuer"],
                audience: configration["JwtOptions:audience"],
                expires: DateTime.Now.AddDays(7),
                claims: claims,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
