using Microsoft.AspNetCore.SignalR;

namespace sga.Services
{
    public class NotificacionesHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("Notification", user, message);
        }
    }
}
