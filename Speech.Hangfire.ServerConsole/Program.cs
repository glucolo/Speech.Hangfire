using Autofac;
using Hangfire;
using Hangfire.Autofac;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Speech.Hangfire.Business;

namespace Speech.Hangfire.ServerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //register Service with AutoFac
            var cBuilder = new ContainerBuilder();
            cBuilder.Register<IBusinessService>(ctx => new BusinessService()).InstancePerBackgroundJob();
            
            var connStr = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json")
                              .Build()
                              .GetConnectionString("Speech.HangFire");

            GlobalConfiguration.Configuration.UseSqlServerStorage(connStr, new SqlServerStorageOptions
            {
                //common configuration to make before v1.8 (for backward compatibility)
                DisableGlobalLocks = true // Migration to Schema 7 is required
            })
            //Configure Hangfire to resolve all dependency for jobs using AutoFac
            .UseAutofacActivator(cBuilder.Build())
            .UseColouredConsoleLogProvider();

            //using (var server = new BackgroundJobServer(new BackgroundJobServerOptions(), JobStorage.Current, new[] { new Deamon() }))
            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadLine();
            }
        }
    }
}