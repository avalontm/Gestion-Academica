using Microsoft.AspNetCore.SignalR;

namespace sga.Services
{
    public class NotificacionesHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("Notification",  message);
        }

        public async Task SendMessageTo(string user_id, string message)
        {
            await this.Clients.User(user_id).SendAsync("Notification", message);
        }
    }
}
