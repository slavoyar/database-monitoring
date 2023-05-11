namespace DatabaseMonitoring.Services.Notification.WebApi.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<SendEmailRequest, MailData>();
        CreateMap<MailData, MailEntity>()
            .ForMember(me => me.Recepients, options => options.MapFrom((md, _) => JsonSerializer.Serialize(md.To)));
    }
}