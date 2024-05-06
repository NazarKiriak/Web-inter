namespace RESTwebAPI.Services.WebPageChecker
{
    using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    // Метод для прийому повідомлень від фонової служби
    public Task ReceiveMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

}
