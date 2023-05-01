using System.Threading.Tasks;

namespace DatabaseMonitoring.Services.Notification.Core.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string recepientEmail, string subject, string body);
}