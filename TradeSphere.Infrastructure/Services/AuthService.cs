namespace TradeSphere.Infrastructure.Services
{
    public class AuthService(UserManager<AppUser> userManager, IConfiguration configration, IRefreshTokenRepository refreshTokenRepository) : IAuthService
    {
        public async Task<string> GenerateJwtToken(AppUser user)
        {
            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!)
        };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var envSecret = Environment.GetEnvironmentVariable("secretKey");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envSecret ?? configration["JwtOptions:secretKey"]!));
            var token = new JwtSecurityToken(
                issuer: configration["JwtOptions:issuer"],
                audience: configration["JwtOptions:audience"],
                expires: DateTime.UtcNow.AddMinutes(15),
                claims: claims,
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(int userId, bool rememberMe)
        {
            var expireOn = rememberMe ? 30 : 7;
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpireOn = DateTime.UtcNow.AddDays(expireOn),
                CreatedOn = DateTime.UtcNow,
                AppUserId = userId
            };
        }

        public async Task<(string AccessToken, RefreshToken RefreshToken)> RefreshTokenAsync(string token)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(token);

            if (refreshToken is null)
                throw new Exception("Invalid refresh token");

            if (refreshToken.RevokedOn.HasValue)
                throw new Exception("Token has been revoked");

            if (refreshToken.ExpireOn < DateTime.UtcNow)
                throw new Exception("Token has expired");

            await refreshTokenRepository.RevokeAsync(refreshToken);

            var user = refreshToken.AppUser;
            var newAccessToken = await GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken(user.Id, refreshToken.RememberMe);
            await refreshTokenRepository.AddAsync(newRefreshToken);

            return (newAccessToken, newRefreshToken);
        }
    }
}
