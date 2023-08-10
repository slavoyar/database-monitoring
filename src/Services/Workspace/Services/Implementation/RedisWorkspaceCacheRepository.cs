namespace DatabaseMonitoring.Services.Workspace.Services.Implementation;

/// <summary>
/// ICashRepository implementation for cashing Workspaces
/// </summary>
public class RedisWorkspaceCacheRepository : ICacheRepository<WorkspaceDto>
{
    private readonly ILogger<RedisWorkspaceCacheRepository> logger;
    private readonly IDistributedCache distributedCache;

    /// <summary>
    /// Ctor
    /// </summary>
    public RedisWorkspaceCacheRepository(ILogger<RedisWorkspaceCacheRepository> logger, IDistributedCache distributedCache)
    {
        this.logger = logger ?? throw new ArgumentNullException();
        this.distributedCache = distributedCache ?? throw new ArgumentNullException();
    }

    /// <inheritdoc />
    public async Task<WorkspaceDto> GetAsync(string key)
    {
        var workspace = await distributedCache.GetStringAsync(key);
        if(workspace == null)
            return null;
        return JsonSerializer.Deserialize<WorkspaceDto>(workspace);
    }

    /// <inheritdoc />
    public async Task SetAsync(string key, WorkspaceDto value)
    {  
        var workspaceDtoAsString = JsonSerializer.Serialize(value);
        logger.LogInformation("Setting new value to cash: {value}", workspaceDtoAsString);
        var options = new DistributedCacheEntryOptions{SlidingExpiration = TimeSpan.FromDays(1)};
        await distributedCache.SetStringAsync(key, workspaceDtoAsString, options);
    }
}