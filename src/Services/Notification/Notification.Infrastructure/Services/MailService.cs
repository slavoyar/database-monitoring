namespace DatabaseMonitoring.Services.Notification.Infrastructure.Services;

public class MailService : IMailService
{
    private readonly ILogger<MailService> logger;
    private readonly MailConfiguration mailConfiguration;
    private readonly IMapper mapper;

    public MailService(
        IOptions<MailConfiguration> mailConfigurationOptions,
        ILogger<MailService> logger,
        IMapper mapper
        )
    {
        this.logger = logger;
        this.mailConfiguration = mailConfigurationOptions.Value;
        this.mapper = mapper;
    }
    public async Task<bool> SendAsync(MailData mailData, CancellationToken cts = default)
    {
        try
        {
            var mail = new MimeMessage();

            //Sender
            mail.From.Add(new MailboxAddress(mailConfiguration.DisplayName, mailData.From ?? mailConfiguration.From));
            mail.Sender = new MailboxAddress(mailData.DispalyName ?? mailConfiguration.DisplayName, mailData.From ?? mailConfiguration.From);

            //Receiver
            foreach (var mailAdress in mailData.To)
                mail.To.Add(MailboxAddress.Parse(mailAdress));

            //Reply to
            if (!string.IsNullOrEmpty(mailData.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

            //Bcc
            if (mailData.Bcc != null)
            {
                foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
            }

            //Cc
            if (mailData.Cc != null)
            {
                foreach (string mailAddress in mailData.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
            }

            //Content
            var body = new BodyBuilder();
            mail.Subject = mailData.Subject;
            body.HtmlBody = mailData.Body;
            mail.Body = body.ToMessageBody();

            //Sending
            var smtp = new SmtpClient();

            if (mailConfiguration.UseSSL)
                await smtp.ConnectAsync(mailConfiguration.Host, mailConfiguration.Port, SecureSocketOptions.SslOnConnect, cts);
            else if (mailConfiguration.UseStartTls)
                await smtp.ConnectAsync(mailConfiguration.Host, mailConfiguration.Port, SecureSocketOptions.StartTls, cts);
            await smtp.AuthenticateAsync(mailConfiguration.Username, mailConfiguration.Password, cts);
            await smtp.SendAsync(mail, cts);
            await smtp.DisconnectAsync(true, cts);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning($"Error while sending message. Reason: {ex.Data}");
            return false;
        }
    }
}