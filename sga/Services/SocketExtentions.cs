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
                User user = User.Find(user_id);

                if(user != null)
                {
                    var absUrl = string.Format("{0}://{1}{2}", context.Request.Scheme, context.Request.Host, user.avatar);
                    user.password = string.Empty;
                    user.avatar = absUrl;
                }

                return user;
            }

            return null;
        }
    }
}
