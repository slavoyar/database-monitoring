using DatabaseMonitoring.Services.Notification.Core.Interfaces;
using DatabaseMonitoring.Services.Notification.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseMonitoring.Services.Notification.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> logger;
    private readonly IMailService mailsService;
    private readonly IMapper mapper;

    public EmailController(
        ILogger<EmailController> logger,
        IMailService mailsService,
        IMapper mapper)
    {
        this.logger = logger;
        this.mailsService = mailsService;
        this.mapper = mapper;
    }

    [HttpPost]
    [Route("Send")]
    public async Task<ActionResult> SendSingleEmailAsync(SendEmailRequest request)
    {
        var mailData = mapper.Map<MailData>(request);
        var cts = new CancellationToken();
        await mailsService.SendAsync(mailData, cts);
        return Ok();
    }
}