namespace LR14.Interfaces
{
    public interface ISendMail
    {
        Task SendMailAsync(string to, string title, string content);
    }
}
