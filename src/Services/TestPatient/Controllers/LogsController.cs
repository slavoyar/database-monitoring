using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestPatient.Data;
using TestPatient.Interfaces;
using System.Net;

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
        [Route("GetLogs")]
        public async Task<IActionResult> GetLogs()
        {
            //--- Check Input Data
            var allLogs = _hangfireDatabaseContext.PatientLogs
                .ToList();

            if ( allLogs == null )
                return BadRequest();

            //--- Return found Users
            return Ok(allLogs);
        }
    }
}