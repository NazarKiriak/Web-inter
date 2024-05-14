using Quartz;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace LR14.Jobs
{
    public class SendMailJob : IJob
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly ILogger _logger;

        public SendMailJob(ISendGridClient sendGridClient, ILogger<SendMailJob> logger)
        {
            _sendGridClient = sendGridClient;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("maxim1202@ukr.net", "Maksym Melnykov"),
                Subject = "Periodic email with SendGrid",
                PlainTextContent = "This is a periodic email sent via Quartz.NET and easy to do anywhere, especially with C# .NET"
            };
            msg.AddTo(new EmailAddress("melnikovmaks1202@gmail.com", "Max"));
            var response = await _sendGridClient.SendEmailAsync(msg);

            _logger.LogInformation(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
        }
    }
}
