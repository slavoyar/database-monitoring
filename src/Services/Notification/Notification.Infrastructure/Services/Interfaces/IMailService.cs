namespace DatabaseMonitoring.Services.Notification.Infrastructure.Services.Interfaces;

public interface IMailService
{
    Task<bool> SendAsync(MailData mailData, CancellationToken cts);
}