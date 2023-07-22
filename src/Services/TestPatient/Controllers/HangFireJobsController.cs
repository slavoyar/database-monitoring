using Hangfire;
using Microsoft.AspNetCore.Mvc;
using TestPatient.Interfaces;

namespace TestPatient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HangFireJobsController : ControllerBase
    {
        private readonly IHangFireJobService _hangfireJobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public HangFireJobsController(
            IHangFireJobService hangFireJobService,
            IBackgroundJobClient backgroundJobClient,
            IRecurringJobManager recurringJobManager)
        {
            _hangfireJobService = hangFireJobService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _hangfireJobService.FireAndForgetJob());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _hangfireJobService.DelayedJob(), TimeSpan.FromSeconds(1));
            return Ok();
        }

        [HttpGet("/ReccuringJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("MiauLogs", () => _hangfireJobService.ReccuringJob(), Cron.Minutely);
            return Ok();
        }

        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _hangfireJobService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _hangfireJobService.ContinuationJob());

            return Ok();
        }
    }
}