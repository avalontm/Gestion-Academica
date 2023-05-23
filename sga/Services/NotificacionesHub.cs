using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using sga.DataBase.Tables;
using System.Diagnostics.CodeAnalysis;

namespace sga.Services
{
    public class UserHub
    {
        public string? id { get; set; }

        public int user_id { get; set; }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificacionesHub : Hub
    {
        static List<UserHub> users = new List<UserHub>();

        public override Task OnConnectedAsync()
        {
            var _user = this.Context.GetHttpContext().GetHubUser();

            if (_user != null)
            {
                users.Add(new UserHub() { id = this.Context.ConnectionId, user_id = _user.id});
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var _user = this.Context.GetHttpContext().GetHubUser();

            if (_user != null)
            {
                var _userhub = users.Where(x=> x.user_id == _user.id).FirstOrDefault();

                if (_userhub != null)
                {
                    users.Remove(_userhub);
                }
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("Notification",  message);
        }

        public async Task SendMessageTo(int user_id, string message)
        {
            var usershub = users.Where(x => x.user_id == user_id);

            foreach (var userhub in usershub)
            {
                await this.Clients.User(userhub.id).SendAsync("Notification", message);
            }
        }
    }
}
