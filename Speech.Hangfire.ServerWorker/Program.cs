using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;

namespace Speech.Hangfire.ServerWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string? connStr = string.Empty;
            
            IHost host = Host.CreateDefaultBuilder(args)
                             .ConfigureAppConfiguration((ctx, cfg) => ctx.Configuration.GetConnectionString("Speech.HangFire"))
                             .ConfigureServices((context, services) =>
                             {
                                 connStr = context.Configuration.GetConnectionString("Speech.HangFire");
                                 services.AddHangfire(cfg =>
                                 {
                                     cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                        .UseColouredConsoleLogProvider()
                                        .UseSimpleAssemblyNameTypeSerializer()
                                        .UseRecommendedSerializerSettings()
                                        .UseSqlServerStorage(connStr, new SqlServerStorageOptions
                                        {
                                            //common configuration to make before v1.8 (for backward compatibility)
                                            DisableGlobalLocks = true // Migration to Schema 7 is required
                                        });
                             
                                 });
                                 services.AddHangfireServer();
                             })
                             .Build();

            host.Run();
        }
    }
}