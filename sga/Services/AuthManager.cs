using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using sga.DataBase.Tables;
using Microsoft.AspNetCore.SignalR.Client;

namespace sga
{
    public static class AuthManager
    {

        public static async Task<User> Auth(this HttpContext context, string email, string password)
        {
            try
            {
                User user = User.Get(email, password);

                if (user == null)
                {
                    return null;
                }

                var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.id.ToString()), new Claim(ClaimTypes.Name, user.email), new Claim(ClaimTypes.Role, user.role_id.ToString()), new Claim(ClaimTypes.SerialNumber, TokenManager.GenerateToken(user.id.ToString())) };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
           
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AUTH] {ex}");
                return null;
            }

        }
    }
}
