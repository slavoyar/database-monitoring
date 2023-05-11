
namespace Notification.Tests.WebApi.Services.Mailservice;

public class SendAsyncTests
{
    private readonly Mock<IRepository<MailEntity>> mailRepository;
    private readonly Mock<IRepository<ErrorSending>> errorSendingRepository;
    private readonly MailService mailservice;
    public SendAsyncTests()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        mailRepository = fixture.Freeze<Mock<IRepository<MailEntity>>>();
        errorSendingRepository = fixture.Freeze<Mock<IRepository<ErrorSending>>>();
        var maper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        fixture.Register<IMapper>(() => new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper());
        mailservice = fixture.Build<MailService>().OmitAutoProperties().Create();
    }

    [Fact]
    public async void SendAsyncTests_OnSending_AddsMessageToTheRepository()
    {
        var mailData = new Fixture().Create<MailData>();

        await mailservice.SendAsync(mailData);

        mailRepository.Verify(r => r.CreateAsync(It.Is<MailEntity>(e => e.Recepients.Contains(mailData.To[0]))));
    }

    [Fact]
    public async void SendAsyncTests_OnError_AddErrorToTheRepository()
    {
        var mailData = new Fixture().Create<MailData>();

        await mailservice.SendAsync(mailData);
        
    }
}