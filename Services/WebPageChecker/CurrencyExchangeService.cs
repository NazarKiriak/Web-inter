namespace RESTwebAPI.Services.WebPageChecker
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Hosting;

    public class CurrencyExchangeService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _apiUrl = "https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_8FHVQGDqLsWDT09FzfdcGCHbOO42qrMm4maJEiYE";

        public CurrencyExchangeService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken); // Delay for 10 minutes

                // Make API call to get currency exchange rates
                var response = await _httpClient.GetAsync(_apiUrl, stoppingToken);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var currencyData = JsonSerializer.Deserialize<CurrencyData>(content, options);

                    // Cache the currency data
                    _cache.Set("CurrencyData", currencyData, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
                }
            }
        }
    }

    public class CurrencyData
    {
        public string BaseCurrency { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }

}
