using System.Net;
using DatabaseMonitoring.Services.Notification.Core.Interfaces;
using DatabaseMonitoring.Services.Notification.Core.Models;
using DatabaseMonitoring.Services.Notification.Infrastructure.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DatabaseMonitoring.Services.Notification.Infrastructure.Services;

public class SmtpEmailSender : IEmailSender
{
    private readonly ILogger<SmtpEmailSender> logger;
    private readonly IOptions<SmtpSettings> smtpSettings1;
    private readonly IRepository<EmailMessage> emailMessageRepository;
    private readonly SmtpSettings smtpSettings;

    public SmtpEmailSender(
        ILogger<SmtpEmailSender> logger, 
        IOptions<SmtpSettings> smtpSettings,
        IRepository<EmailMessage> emailMessageRepository)
    {
        this.logger = logger;
        smtpSettings1 = smtpSettings;
        this.emailMessageRepository = emailMessageRepository;
        this.smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string recepientEmail, string subject, string body)
    {
        var msg = new MimeMessage();
        msg.From.Add(MailboxAddress.Parse(smtpSettings.SenderEmail));
        msg.To.Add(MailboxAddress.Parse(recepientEmail));
        msg.Subject = subject;
        msg.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
        {
            Text = body
        };

        var client = new SmtpClient();

        try
        {
            await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpSettings.Username, smtpSettings.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
            logger.LogInformation("Email sent successfully");
        }
        catch (Exception e)
        {
            logger.LogWarning($"Exception while sending email: {e.Message}");
        }
        finally
        {
            client.Dispose();
        }
    }
}