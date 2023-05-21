using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using sga.DataBase.Tables;
using sga.Models;
using System.Security.Claims;

namespace sga
{
    public static class PageAuthExtention
    {
        public static async Task<AuthModel> GetAuth(this ComponentBase page, AuthenticationStateProvider GetAuthenticationStateAsync)
        {
            AuthModel auth = new AuthModel();

            var authstate = await GetAuthenticationStateAsync.GetAuthenticationStateAsync();
            var _user = authstate.User;

            auth.status = _user.Identity?.IsAuthenticated ?? false;

            if (auth.status)
            {
                int user_id = int.Parse(_user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                auth.user = User.Find(user_id);

                if (auth.user != null)
                {
                    auth.token = _user.FindFirst(ClaimTypes.SerialNumber)?.Value;
                }
            }

            return auth;
        }
    }
}
