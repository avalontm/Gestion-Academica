using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using sga.DataBase.Tables;

namespace sga.Sockets
{
    public class UserHub
    {
        public string? id { get; set; }
        public int user_id { get; set; }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificacionesHub : Hub
    {
        readonly static ConnectionMapping<int> _connections = new ConnectionMapping<int>();

        User GetSession()
        {
            return Context.GetHttpContext().GetHubUser();
        }

        public override Task OnConnectedAsync()
        {
            User _user = GetSession();

            if (_user != null)
            {
                _connections.Add(_user.id, Context.ConnectionId);
            }

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            User _user = GetSession();

            if (_user != null)
            {
                _connections.Remove(_user.id, Context.ConnectionId);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAll(string message)
        {
            await Clients.All.SendAsync("Notification", message);
            await Task.Delay(100);
            Console.WriteLine($"[SendMessageAll] {message}");
        }

        public async Task SendMessageTo(int user_id, string message)
        {
            foreach (var connectionId in _connections.GetConnections(user_id))
            {
                Clients.Client(connectionId).SendAsync("Notification", message);
            }
            await Task.Delay(100);
            Console.WriteLine($"[SendMessageTo] {user_id} | {message}");
        }
    }
}
