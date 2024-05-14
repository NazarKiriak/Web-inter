using LR14.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace LR14.BackgroundServices
{
    public class NotifyService : BackgroundService
    {
        private readonly IHubContext<NotifyHub> _hubContext;

        public NotifyService(IHubContext<NotifyHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Maksym", "Hello, users!", stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
