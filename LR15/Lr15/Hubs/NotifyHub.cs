using Microsoft.AspNetCore.SignalR;

namespace LR14.Hubs
{
    public class NotifyHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task Notify(string notification)
        {
            await Clients.All.SendAsync("ReceiveNotification", notification);
        }
    }
}
