using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace sga
{
    public static class TokenManager
    {
        public static string GenerateToken(string userId)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(Program.Configuration["JwtKey"]));
            int SesionExpire = int.Parse(Program.Configuration["SesionExpire"]);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId)
        };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("lucargo", "lucargo", claims, expires: DateTime.UtcNow.AddDays(SesionExpire), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
