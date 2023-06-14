

namespace DatabaseMonitoring.Services.Notification.WebApi.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly ILogger<WorkspaceService> logger;
    private readonly int retryCount;
    private readonly HttpClient client;
    public readonly Policy policy;

    public WorkspaceService(
        ILogger<WorkspaceService> logger,
        IHttpClientFactory httpClientFactory,
        int retryCount = 3
    )
    {
        this.logger = logger;
        this.retryCount = retryCount;
        client = httpClientFactory.CreateClient("WorkspaceService");
        policy = Policy.Handle<Exception>()
            .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(200, retryAttempt)), (ex, time) => 
            {
                logger.LogInformation(ex, "WorkspaceService could not get data after {TimeOut}s ({ExceptionMessage}", $"{time.TotalSeconds:n1}", ex.Message);
            }
        );
    }

    public async Task<IEnumerable<Guid>> GetUsersAssociatedWithServer(Guid serverId)
    {
        logger.LogInformation($"WorkspaceSerivce is requesting {nameof(this.GetUsersAssociatedWithServer)}");

        var result = await policy.Execute(async () => {
            return await client.GetFromJsonAsync<IEnumerable<Guid>>($"/workspace/UsersByServerId/{serverId}");
        });

        return result;
    }

    public async Task<IEnumerable<Guid>> GetWorkspaceUsers(Guid workspaceId)
    {
        logger.LogInformation($"WorkspaceSerivce is requesting {nameof(this.GetWorkspaceUsers)}");

        var result = await policy.Execute(async () => {
            return await client.GetFromJsonAsync<IEnumerable<Guid>>($"/workspace/{workspaceId}/users");
        });

        return result;
    }
}