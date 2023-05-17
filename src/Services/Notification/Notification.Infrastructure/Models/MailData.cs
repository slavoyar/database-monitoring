namespace DatabaseMonitoring.Services.Notification.Infrastructure.Models;

public class MailData
{
    //Receiver
    public List<string> To { get; set; }
    public List<string> Bcc { get; set; }
    public List<string> Cc { get; set; }

    //Sender
    public string From { get; set; }
    public string DispalyName { get; set; }
    public string ReplyTo { get; set; }
    public string ReplyToName { get; set; }

    //Content
    public string Subject { get; set; }
    public string Body { get; set; }

    public MailData()
    {

    }
    public MailData(List<string> to, string subject, string body = null, string from = null, string dispalyName = null, string replyTo = null, string replyToName = null, List<string> bcc = null, List<string> cc = null)
    {
        To = to;
        Subject = subject;
        Body = body;
        From = from;
        DispalyName = dispalyName;
        ReplyTo = replyTo;
        ReplyToName = replyToName;
        Bcc = bcc;
        Cc = cc;
    }
}