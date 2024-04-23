using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

public class CurrencyHub : Hub
{
    private readonly CurrencyService _currencyService;

    public CurrencyHub(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public async Task GetCurrencyRates()
    {
        await _currencyService.SendCurrencyRatesToClients();
    }
}

