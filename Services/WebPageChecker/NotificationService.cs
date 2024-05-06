namespace RESTwebAPI.Services.WebPageChecker
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class NotificationService : BackgroundService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Реалізуйте вашу логіку надсилання повідомлень тут
                // Наприклад, відправка повідомлень до всіх підключених клієнтів через SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Служба сповіщень", $"Повідомлення з фонової служби. Час: {DateTime.Now}");

                // Затримка перед наступною ітерацією
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }

}
