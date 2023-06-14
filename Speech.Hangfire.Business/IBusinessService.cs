using Hangfire;

namespace Speech.Hangfire.Business
{
    public interface IBusinessService
    {
        Task JobUnderTheWood();

        [AutomaticRetry(Attempts = 0)]
        Task FailingJobWithoutRetry();
    }
}