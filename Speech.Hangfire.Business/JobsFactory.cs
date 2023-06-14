
using Hangfire;
using Hangfire.Server;

namespace Speech.Hangfire.Business
{
    public class JobsFactory
    {
        //public static Expression<Action> SingleJob => () => Console.WriteLine($"This is a simple job running at {DateTime.Now.ToShortTimeString()}");
        //public static Expression<Action> Scheduledjob => () => Console.WriteLine($"This is a scheduled job running at {DateTime.Now.ToShortTimeString()}");
        //public static Expression<Action> Recurringjob => () => Console.WriteLine($"This is a recurring job running at {DateTime.Now.ToShortTimeString()}");
        //public static Expression<Action<string>> Continuationjob => (parent) => Console.WriteLine($"This is a continuation of {parent} job running at {DateTime.Now.ToShortTimeString()}");
        //public static void ContinuationjobFunc(string parent) => Console.WriteLine($"This is a continuation of {parent} job running at {DateTime.Now.ToShortTimeString()}");

        public static void SingleJob() => Console.WriteLine($"This is a simple job running at {DateTime.Now.ToShortTimeString()}");
        public static void Scheduledjob() => Console.WriteLine($"This is a scheduled job running at {DateTime.Now.ToShortTimeString()}");
        public static void Recurringjob() => Console.WriteLine($"This is a recurring job running at {DateTime.Now.ToShortTimeString()}");

        //Push parent result to job continuation ("AntecedentResult")
        //[ContinuationsSupport(pushResults: true)]
        public static void Continuationjob(string parent, PerformContext ctx)
        {
            var parentjob = ctx.Connection.GetJobData(parent);
            var parentState = parentjob.State;
            Console.WriteLine($"This is a continuation of job \"{parent}\" running at {DateTime.Now.ToShortTimeString()};{Environment.NewLine}Parent job has termintated with \"{parentState}\" state");
        }

        public static void Failingjob()
        {
            Console.WriteLine($"This is a wrong job running at {DateTime.Now.ToShortTimeString()} and will fail asap");
            throw new NotImplementedException();
        }
    }
}
