using Serilog;
using Microsoft.Extensions.Hosting;

namespace MovieReservationSystem.Extensions
{
    public static class LoggingExtensions
    {
        public static void ConfigureLogging(this IHostBuilder hostBuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(@"C:\Users\Kaan\Desktop\logsnew\log.txt", rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} (Category ID: {CategoryId}){NewLine}{Exception}")
                .CreateLogger();

            Log.Information("Application Starting Up");
        }
    }
}