namespace DatabaseMonitoring.Services.Notification.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SendEmailRequest, MailData>();

        CreateMap<NotificationDto, UnreadNotification>();
    }
}