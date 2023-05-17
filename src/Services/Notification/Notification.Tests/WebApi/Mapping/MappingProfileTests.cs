namespace Notification.Tests.WebApi.Mapping;

public class MappingProfileAsyncTests
{
    private readonly IMapper mapper;
    public MappingProfileAsyncTests()
    {
        mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
    }

    [Fact]
    public void Mapper_CorrectMaps_SendEmailRequest_To_MailData()
    {
        //a
        var request = new Fixture().Create<SendEmailRequest>();

        //a
        var mailData = mapper.Map<MailData>(request);

        //a
        mailData.To.Should().NotBeEmpty().And.HaveCount(request.To.Count()).And.Contain(request.To);
        mailData.Body.Should().BeSameAs(request.Body);
        mailData.Subject.Should().BeSameAs(request.Subject);
    }

    [Fact]
    public void Mapper_CorrectMaps_MailData_To_MailEntity()
    {
        var mailData = new Fixture().Create<MailData>();
        
        var mailEntity = mapper.Map<MailEntity>(mailData);

        mailEntity.Body.Should().BeSameAs(mailData.Body);
        var a = mailEntity.Recepients;
        var b = JsonSerializer.Serialize(mailData.To);
        mailEntity.Recepients.Should().Be(JsonSerializer.Serialize(mailData.To));
    }
}