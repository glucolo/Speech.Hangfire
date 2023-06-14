using Hangfire.Annotations;
using Hangfire.Server;

namespace Speech.Hangfire.Business
{
    public class Deamon : IBackgroundProcess
    {
        public void Execute([NotNull] BackgroundProcessContext context)
        {
            Console.WriteLine($"Deamon write at {DateTime.Now.ToShortTimeString()}");
            context.Wait(TimeSpan.FromSeconds(15));
        }
    }
}
