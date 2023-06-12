

namespace DatabaseMonitoring.Services.Workspace.Services.Implementation;

/// <summary>
/// Service for working with workspaces, users and servers
/// </summary>
public class WorkspaceService : IWorkspaceService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IApplicationEventService applicationEventSerivce;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="mapper">Mapper with configured profiles</param>
    /// <param name="unitOfWork">Unit of work impl</param>
    /// <param name="applicationEventSerivce">Event bus impl</param>
    public WorkspaceService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IApplicationEventService applicationEventSerivce
    )
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.applicationEventSerivce = applicationEventSerivce;
    }

    ///<inheritdoc/>
    public async Task AddServerToWorkspaceAsync(Guid workspaceId, Guid serverId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(x => x.Id == workspaceId).Include(x => x.Servers).FirstOrDefaultAsync();
        var newServer = new Server{OuterId = serverId, Workspace = workspace};
        await unitOfWork.Servers.CreateAsync(newServer);
        await unitOfWork.Workspaces.SaveChangesAsync();
    
        var @event = new ServerAddedToWorkspaceAppEvent
        {
            ServerId = newServer.OuterId,
            WorkspaceId = workspace.Id 
        };
        applicationEventSerivce.PublishThroughEventBusAsync(@event);
    }

    /// <inheritdoc/>
    public async Task AddUserToWorkspaceAsync(Guid workspaceId, Guid userId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(x => x.Id == workspaceId).Include(x => x.Users).FirstOrDefaultAsync();
        var newUser = new User{OuterId = userId, Workspace = workspace};
        await unitOfWork.Users.CreateAsync(newUser);
        await unitOfWork.Workspaces.SaveChangesAsync();

        var @event = new UserAddedToWorkspaceAppEvent
        {
            UserId = newUser.OuterId,
            WorkspaceId = workspace.Id 
        };
        applicationEventSerivce.PublishThroughEventBusAsync(@event);
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateWorkspaceAsync(WorkspaceDto workspaceDto)
    {
        var workspaceEntity = mapper.Map<WorkspaceEntity>(workspaceDto);
        var id = await unitOfWork.Workspaces.CreateAsync(workspaceEntity);
        await unitOfWork.Workspaces.SaveChangesAsync();
        return id;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteWorkspaceAsync(Guid workspaceId)
    {
        await unitOfWork.Workspaces.DeleteAsync(workspaceId);
        return await unitOfWork.Workspaces.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<WorkspaceDto> GetWorkspaceByIdAsync(Guid workspaceId)
    {
        var entity = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Users).Include(w => w.Servers).FirstAsync();
        var workspaceDto = mapper.Map<WorkspaceDto>(entity);
        return workspaceDto;

    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Guid>> GetUsersAssociatedWithServer(Guid serverId)
    {
        var users = await unitOfWork.Workspaces.GetAll()
            .Include(w => w.Servers)
            .Where(w => w.Servers.Any(s => s.OuterId == serverId))
            .Include(w => w.Users)
            .Select(w => w.Users.Select(u => u.OuterId))
            .SelectMany(id => id).ToListAsync();
        return users;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Guid>> GetWorkspaceServersAsync(Guid workspaceId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll()
            .Where(w => w.Id == workspaceId)
            .Include(w => w.Servers)
            .FirstAsync();
        var servers = workspace.Servers.Select(s => s.OuterId);
        return servers;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Guid>> GetWorkspaceUsersAsync(Guid workspaceId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll()
            .Where(w => w.Id == workspaceId)
            .Include(w => w.Users)
            .FirstAsync();
        var servers = workspace.Users.Select(s => s.Id);
        return servers;
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveServerFromWorkspaceAsync(Guid workspaceId, Guid serverId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Servers).FirstAsync();
        var server = workspace.Servers.First(x => x.OuterId == serverId);
        await unitOfWork.Servers.DeleteAsync(server.Id);

        if(!await unitOfWork.Servers.SaveChangesAsync())
            return false;

        var @event = new ServerRemovedFromWorkspaceAppEvent
            {
                ServerId = server.OuterId,
                WorkspaceId = workspace.Id 
            };
        applicationEventSerivce.PublishThroughEventBusAsync(@event);

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveUserFromWorkspaceAsync(Guid workspaceId, Guid userId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Users).FirstAsync();
        var user = workspace.Users.First(x => x.OuterId == userId);
        await unitOfWork.Servers.DeleteAsync(user.Id);
        if(!await unitOfWork.Servers.SaveChangesAsync())
            return false;

        var @event = new UserRemovedFromWorkspaceAppEvent
            {
                UserId = user.OuterId,
                WorkspaceId = workspace.Id 
            };
        applicationEventSerivce.PublishThroughEventBusAsync(@event);

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateWorkspaceAsync(Guid workspaceId, WorkspaceDto workspaceDto)
    {
        var workspace = await unitOfWork.Workspaces.GetAsync(workspaceId);
        workspace.Description = workspaceDto.Description;
        workspace.Name = workspaceDto.Name;
        unitOfWork.Workspaces.Update(workspace);
        return await unitOfWork.Workspaces.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<bool> WorkspaceExists(Guid id)
        => (await unitOfWork.Workspaces.GetAsync(id)) != null;

}