using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTwebAPI.Services.WebPageChecker;
using RESTwebAPI.Models;

public class EmailNotificationService : BackgroundService
{
    private readonly ILogger<EmailNotificationService> _logger;
    private readonly HealthChecksDb _dbContext;
    private readonly EmailService _emailService;

    public EmailNotificationService(ILogger<EmailNotificationService> logger, HealthChecksDb dbContext, EmailService emailService)
    {
        _logger = logger;
        _dbContext = dbContext;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EmailNotificationService is starting.");

        stoppingToken.Register(() =>
            _logger.LogInformation("EmailNotificationService is stopping."));

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                    foreach (var entry in _dbContext.ChangeTracker.Entries<HealthCheckResult>())
                {
                    if (entry.State == EntityState.Added)
                    {
                        await _emailService.SendEmailAsync("maksimkamychko@gmail.com", "Новий запис додано", "Текст повідомлення про новий запис.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing database changes.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
        }
    }
}
