namespace RESTwebAPI.Services.WebPageChecker
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Quartz;

public class MyJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.maxim.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("maksimkamychko123@gmail.com", "max_car_cool"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("maksimkamychko123@gmail.com"),
                    Subject = "Test Email",
                    Body = "This is a test email sent from Quartz.NET job.",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add("maksimkamychko@gmail.com");

                // Відправлення електронного листа
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }
    }


}
