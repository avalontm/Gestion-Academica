using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using sga.DataBase.Tables;

namespace sga
{
    public static class NotificationManager
    {
        public static async Task SendAll(NavigationManager navigationManager, string token, string message)
        {
            HubConnection _hubConnection = new HubConnectionBuilder()
                     .WithUrl(navigationManager.ToAbsoluteUri("/notificaciones"), options =>
                     {
                         options.AccessTokenProvider = () => Task.FromResult(token);
                     })
            .WithAutomaticReconnect()
            .Build();

            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("SendMessageAll", message);
            await _hubConnection.StopAsync();
        }

        public static async Task SendTo(NavigationManager navigationManager, string token, int user_id, string message)
        {
            HubConnection _hubConnection = new HubConnectionBuilder()
                     .WithUrl(navigationManager.ToAbsoluteUri("/notificaciones"), options =>
                     {
                         options.AccessTokenProvider = () => Task.FromResult(token);
                     })
            .WithAutomaticReconnect()
            .Build();

            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("SendMessageTo", user_id, message);
            await _hubConnection.StopAsync();
        }
    }
}
