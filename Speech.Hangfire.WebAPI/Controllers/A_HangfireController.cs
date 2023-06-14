using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Speech.Hangfire.Business;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Speech.Hangfire.WebAPI.Controllers
{
    /// <summary>
    /// Static methods as jobs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class A_HangfireController : ControllerBase
    {
        private readonly ILogger<A_HangfireController> _logger;
        private readonly IBackgroundJobClient _hangfirejob;
        private readonly IRecurringJobManagerV2 _handfireRecurring;

        public A_HangfireController(ILogger<A_HangfireController> logger, IBackgroundJobClient hfJob, IRecurringJobManagerV2 hfRec)
        {
            this._logger = logger;
            this._hangfirejob = hfJob;
            this._handfireRecurring = hfRec;
        }


        [HttpPost("FireAndForget")]
        [SwaggerOperation(
            Summary = "Create a new job",
            Description = "New simple job from static method with fire and forget policy",
            OperationId = "FireAndForget")]
        [SwaggerResponse(200, "Id del nuovo job", typeof(string))]
        public IActionResult FireAndForget()
        {
            var jobid = _hangfirejob.Enqueue(() => JobsFactory.SingleJob());
            return Ok($"Job: {jobid}");
        }


        [HttpPost("Schedule")]
        [SwaggerOperation(
            Summary = "Create a new scheduled job",
            Description = "New simple job scheduled after 30 seconds, with fire and forget policy",
            OperationId = "Schedule")]
        [SwaggerResponse(200, "Id del nuovo job", typeof(string))]
        public IActionResult Schedule()
        {
            var jobid = _hangfirejob.Schedule(() => JobsFactory.Scheduledjob(), TimeSpan.FromSeconds(30));
            return Ok($"Scheduled Job: {jobid}");
        }


        [HttpPost("Recurring")]
        [SwaggerOperation(
            Summary = "Create a new recurring job",
            Description = "New recurring job fired every minutes",
            OperationId = "Recurring")]
        [SwaggerResponse(204)]
        public IActionResult Recurring()
        {
            _handfireRecurring.AddOrUpdate("newRecurringjob", () => JobsFactory.Recurringjob(), Cron.Minutely);
            return NoContent();
        }


        [HttpPost("Continuation")]
        [SwaggerOperation(
            Summary = "Create a continuation job",
            Description = "New simple job with a continuation policy",
            OperationId = "Continuation")]
        [SwaggerResponse(204)]
        public IActionResult Continuation()
        {
            var jobid = _hangfirejob.Enqueue(() => JobsFactory.SingleJob());
            //when there is a PerformContext as parameter, Hangfire inject execution context to continuation method
            _hangfirejob.ContinueJobWith(jobid, () => JobsFactory.Continuationjob(jobid, null), JobContinuationOptions.OnAnyFinishedState);
            return NoContent();
        }


        [HttpPost("Failing")]
        [SwaggerOperation(
            Summary = "Create a failing job",
            Description = "New failing job with a standard retry policy",
            OperationId = "Failing")]
        [SwaggerResponse(204)]
        public IActionResult Failing()
        {
            var jobid = _hangfirejob.Enqueue(() => JobsFactory.Failingjob());
            return NoContent();
        }
    }
}
