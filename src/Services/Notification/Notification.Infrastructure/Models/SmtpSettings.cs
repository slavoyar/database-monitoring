namespace DatabaseMonitoring.Services.Notification.Infrastructure.Models;

public class SmtpSettings
{
    public const string Smtp = "Smtp";
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderEmail { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}