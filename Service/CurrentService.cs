using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class CurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly IHubContext<CurrencyHub> _hubContext;

    public CurrencyService(HttpClient httpClient, IHubContext<CurrencyHub> hubContext)
    {
        _httpClient = httpClient;
        _hubContext = hubContext;
    }

    public async Task SendCurrencyRatesToClients()
    {
        var response = await _httpClient.GetAsync("https://api.freecurrencyapi.com/v1/latest?apikey=fca_live_8FHVQGDqLsWDT09FzfdcGCHbOO42qrMm4maJEiYE");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var currencyRates = JObject.Parse(content)["data"].ToString();

        await _hubContext.Clients.All.SendAsync("ReceiveCurrencyRates", currencyRates);
    }
}
