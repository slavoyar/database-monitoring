using DatabaseMonitoring.Services.Notification.Core.Interfaces;
using DatabaseMonitoring.Services.Notification.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMonitoring.Services.Notification.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> logger;
    private readonly IEmailSender sender;

    public EmailController(ILogger<EmailController> logger, IEmailSender sender)
    {
        this.logger = logger;
        this.sender = sender;
    }

    [HttpPost]
    [Route("send")]
    public async Task<ActionResult> SendEmailMessageAsync(SendEmailRequest request)
    {
        try
        {
            await sender.SendEmailAsync(request.Recepient, request.Subject, request.Body);
            return Ok();
        }
        catch(Exception e)
        {
            logger.LogWarning(e.Message);
            return StatusCode(500,"An error occured while sending email");
        }
    }
}