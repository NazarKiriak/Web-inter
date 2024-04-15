using Serilog;
using Serilog.Events;

namespace RESTwebAPI.Models
{
    public class LoggerSetup
    {
        public static async Task SetupLoggerAsync()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:7021")
            .CreateLogger();
            
            Log.Information("Hello!");
            Log.Warning("Something unexpected happened");
            Log.Fatal("The application encountered a fatal error and must exit");
         
            var user = new User { FirstName = "John", LastName = "Jonson" };
            Log.Information("User {@User} logged in at {LoginTime}", user, DateTime.Now);

            int a = 10, b = 2;
            try
            {
                Log.Debug("Dividing {A} by {B}", a, b);
                Console.WriteLine(a / b);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
