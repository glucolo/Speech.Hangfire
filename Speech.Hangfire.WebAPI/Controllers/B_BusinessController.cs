using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Speech.Hangfire.Business;
using Swashbuckle.AspNetCore.Annotations;

namespace Speech.Hangfire.WebAPI.Controllers
{
    /// <summary>
    /// Instance methods as jobs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class B_BusinessController : ControllerBase
    {
        private readonly ILogger<B_BusinessController> logger;
        private readonly IBackgroundJobClient jobClient;
        private readonly IRecurringJobManager recurringClient;

        public B_BusinessController(ILogger<B_BusinessController> logger, IBackgroundJobClient jobClient, IRecurringJobManager recurringClient)
        {
            this.logger = logger;
            this.jobClient = jobClient;
            this.recurringClient = recurringClient;
        }

        [HttpPost("FireAndForget")]
        [SwaggerOperation(
            Summary = "Create a new job",
            Description = "New simple job from instance method with fire and forget policy",
            OperationId = "FireAndForget")]
        [SwaggerResponse(204)]
        public IActionResult FireAndForget()
        {
            //dependency injection risolta da hangfire
            jobClient.Enqueue<IBusinessService>(srv => srv.JobUnderTheWood());
            return NoContent();
        }

        [HttpPost("Failing")]
        [SwaggerOperation(
            Summary = "Create a failing job",
            Description = "New failing job without retry policy",
            OperationId = "Failing")]
        [SwaggerResponse(204)]
        public IActionResult Failing()
        {
            jobClient.Enqueue<IBusinessService>(srv => srv.FailingJobWithoutRetry());
            return NoContent();
        }



        
        [HttpPost("CRON")]
        [SwaggerOperation(
            Summary = "Create a new recurring job",
            Description = "New recurring job scheduled with CRON expression",
            OperationId = "Recurring")]
        [SwaggerResponse(204)]
        public IActionResult CRON(
            [FromQuery, SwaggerParameter("Example: every hours from 15th to 25th of september")]
            string cron= "0 * 15-25 9 ?")
        {
            recurringClient.AddOrUpdate<IBusinessService>("cron_job", srv => srv.JobUnderTheWood(), cron);
            return NoContent();
        }
    }
}
