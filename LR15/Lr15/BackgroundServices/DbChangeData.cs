using LR14.Interfaces;
using LR14.Models;
using Microsoft.EntityFrameworkCore;

namespace LR14.BackgroundServices
{
    public class DbChangeData : BackgroundService
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISendMail _sendMail;

        public DbChangeData(IAppDbContext appDbContext, ISendMail sendMail)
        {
            _appDbContext = appDbContext;
            _sendMail = sendMail;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _appDbContext.ChangeTracker.Tracked += async (sender, e) =>
            {
                if (e.Entry.Entity is MusicalInstruments && e.Entry.State == EntityState.Added)
                {
                    await _sendMail.SendMailAsync("melnikovmaks1202@gmail.com", "New data added", "A new data added to the MusicalInstrumentsDB.");
                }
            };
            while (!stoppingToken.IsCancellationRequested)
            {
                await _appDbContext.SaveChangesAsync(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
