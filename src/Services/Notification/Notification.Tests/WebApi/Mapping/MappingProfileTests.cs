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
}