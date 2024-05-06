namespace RESTwebAPI.Services.WebPageChecker
{
    using Microsoft.Extensions.Hosting;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebPageChecker : BackgroundService
    {
        private readonly string[] _webPages = { "https://example.com", "https://example.org" };
        private readonly HttpClient _httpClient;
        private readonly string _logFilePath = "webpage_checker.log";

        public WebPageChecker(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var page in _webPages)
                {
                    try
                    {
                        var response = await _httpClient.GetAsync(page, stoppingToken);
                        var isSuccessStatusCode = response.IsSuccessStatusCode;
                        var logMessage = $"{DateTime.UtcNow}: {page} {(isSuccessStatusCode ? "is reachable" : "is not reachable")}";
                        LogResult(logMessage);
                    }
                    catch (Exception ex)
                    {
                        LogResult($"{DateTime.UtcNow}: Error occurred while checking {page}: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // Wait for 10 minutes
            }
        }

        private void LogResult(string message)
        {
            try
            {
                using (var writer = File.AppendText(_logFilePath))
                {
                    writer.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while writing to log file: {ex.Message}");
            }
        }
    }

}
