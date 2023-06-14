
namespace Speech.Hangfire.Business
{
    public class BusinessService : IBusinessService
    {
        private readonly TextWriter writer;

        public BusinessService()
        {
            this.writer = Console.Out;
        }
        public BusinessService(TextWriter writer)
        {
            this.writer = writer;
        }
        public async Task JobUnderTheWood()
        {
            writer.WriteLine($"Start {nameof(JobUnderTheWood)} of {nameof(BusinessService)} service - {DateTime.Now.ToShortTimeString()}");
            await Task.Delay(3000);
            writer.WriteLine($"End {nameof(JobUnderTheWood)} of {nameof(BusinessService)} service - {DateTime.Now.ToShortTimeString()}");
        }

        public async Task FailingJobWithoutRetry()
        {
            await Task.FromResult(0);
            throw new NotImplementedException();
        }
    }
}
