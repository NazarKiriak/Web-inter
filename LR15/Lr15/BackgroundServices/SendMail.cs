using LR14.Interfaces;

namespace LR14.BackgroundServices
{
    public class SendMail : ISendMail
    {
        public async Task SendMailAsync(string to, string title, string content)
        {
            Console.WriteLine($"Email sending to {to}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Content: {content}");
            Console.WriteLine($"\n");
            await Task.CompletedTask;
        }
    }
}
