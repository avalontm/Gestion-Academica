using sga.DataBase.Tables;
using System.Security.Claims;

namespace sga
{
    public static class SocketExtentions
    {
        public static User GetHubUser(this HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                int user_id = int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                User client = User.Find(user_id);
                return client;
            }

            return null;
        }
    }
}
