using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestPatient.Data;
using TestPatient.Interfaces;
using System.Net;
using TestPatient.Models;

namespace TestPatient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly HangfireContext _hangfireDatabaseContext;

        public LogsController(
            HangfireContext hangfireDatabaseContext)
        {
            _hangfireDatabaseContext = hangfireDatabaseContext;
        }

        /// <summary>
        /// Get all logs
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success reading</response>
        /// <response code="400">Data has missing/invalid values</response>
        /// <response code="401">Error while authorizing user, maybe you are not authorized</response>
        [HttpGet]
        public async Task<IActionResult> Logs()
        {
            //--- Check Input Data
            var allLogs = _hangfireDatabaseContext.PatientLogs.Where(x => x.Sended == 0)
                .ToList();

            if (allLogs == null)
                return BadRequest();

            allLogs.ForEach(x => x.Sended = 1);
            _hangfireDatabaseContext.PatientLogs.UpdateRange(allLogs);
            await _hangfireDatabaseContext.SaveChangesAsync();

            var sendingLogs = allLogs.Select(
                x => new SendingLogsModel
                {
                    Id = x.Id,
                    ServerId = x.ServerId,
                    CriticalStatus = x.CriticalStatus,
                    ErrorState = x.ErrorState,
                    ServiceType = x.ServiceType,
                    ServiceName = x.ServiceName,
                    CreatedAt = x.CreatedAt,
                    Message = x.Message
                }
                ).ToList();

            //--- Return found Users
            return Ok(sendingLogs);
        }
    }
}