using LR14.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace LR14.BackgroundServices
{
    public class ExchangeRateService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ExchangeRateService> _logger;

        public ExchangeRateService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, ILogger<ExchangeRateService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var client = _httpClientFactory.CreateClient();
                try
                {
                    var response = await client.GetAsync("https://v6.exchangerate-api.com/v6/1aa452d30d3d382c9bd73cea/latest/USD", stoppingToken);
                    if (response.IsSuccessStatusCode)
                    {
                        var rates = await response.Content.ReadAsAsync<ExchangeRates>(stoppingToken);
                        _memoryCache.Set("ExchangeRates", rates, TimeSpan.FromMinutes(30));
                        string jsonData = JsonConvert.SerializeObject(rates.Conversion_Rates);

                        _logger.LogInformation($"New Rates: {jsonData}");
                        _logger.LogInformation("\n");
                    }
                    else
                    {
                        _logger.LogWarning($"Failed to retrieve exchange rates");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong while fetching exchange rates: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            }
        }
    }
}
