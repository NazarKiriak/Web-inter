namespace LR14.BackgroundServices
{
    public class WebsiteAvailabilityService : BackgroundService
    {
        private readonly ILogger<WebsiteAvailabilityService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _url;

        public WebsiteAvailabilityService(ILogger<WebsiteAvailabilityService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
            _url = "https://moodle3.chmnu.edu.ua/course/view.php?id=34797";
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var response = await _httpClient.GetAsync(_url);
                    var isHealthy = response.IsSuccessStatusCode;
                    var logMessage = $"Health check for {_url} at {DateTime.Now}: {(isHealthy ? "Healthy" : "Unhealthy")}";
                    _logger.LogInformation(logMessage);

                    await File.AppendAllTextAsync("website_availability.log", $"{logMessage}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred during health check");
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
