

namespace DatabaseMonitoring.Services.Workspace.Services.Implementation;

/// <summary>
/// Service for working with workspaces, users and servers
/// </summary>
public class WorkspaceService : IWorkspaceService
{
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IApplicationEventService applicationEventSerivce;
    private readonly ICacheRepository<WorkspaceDto> workspaceCacheReposotory;

    /// <summary>
    /// Contructor
    /// </summary>
    /// <param name="mapper">Mapper with configured profiles</param>
    /// <param name="unitOfWork">Unit of work impl</param>
    /// <param name="applicationEventSerivce">Event bus impl</param>
    /// <param name="workspaceCacheReposotory">Workspace caching repository</param>
    public WorkspaceService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IApplicationEventService applicationEventSerivce,
        ICacheRepository<WorkspaceDto> workspaceCacheReposotory
    )
    {
        this.mapper = mapper ?? throw new ArgumentNullException();
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        this.applicationEventSerivce = applicationEventSerivce ?? throw new ArgumentNullException();
        this.workspaceCacheReposotory = workspaceCacheReposotory ?? throw new ArgumentNullException();
    }

    ///<inheritdoc/>
    /// <summary>
    /// Adds server to workspace and sends an event if there are users in this workspace
    /// </summary>
    /// <param name="workspaceId">Workspace identifer</param>
    /// <param name="serverId">Server identifer</param>
    /// <returns></returns>
    public async Task AddServerToWorkspaceAsync(Guid workspaceId, Guid serverId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(x => x.Id == workspaceId).Include(x => x.Servers).Include(x => x.Users).FirstOrDefaultAsync();
        var newServer = new Server { OuterId = serverId, Workspace = workspace };
        await unitOfWork.Servers.CreateAsync(newServer);
        await unitOfWork.Workspaces.SaveChangesAsync();

        var workspaceDto = mapper.Map<WorkspaceDto>(workspace);
        await workspaceCacheReposotory.SetAsync(workspace.Id.ToString(), workspaceDto);

        if (!workspace.Users.Any())
            return;
        var @event = new ServerAddedToWorkspaceAppEvent
        {
            ServerId = newServer.OuterId,
            WorkspaceId = workspace.Id,
            UsersId = workspace.Users.Select(x => x.OuterId)
        };
        await applicationEventSerivce.PublishThroughEventBus(@event);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Adds user to workspace and sends an event if there are two or more users in it
    /// </summary>
    /// <param name="workspaceId">Workspace identifer</param>
    /// <param name="userId">User identifer</param>
    /// <returns></returns>
    public async Task AddUserToWorkspaceAsync(Guid workspaceId, Guid userId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(x => x.Id == workspaceId).Include(x => x.Users).Include(x => x.Servers).FirstOrDefaultAsync();
        bool noUsersInWorkspace = !workspace.Users.Any();
        var newUser = new User { OuterId = userId, Workspace = workspace };
        await unitOfWork.Users.CreateAsync(newUser);
        await unitOfWork.Workspaces.SaveChangesAsync();

        var workspaceDto = mapper.Map<WorkspaceDto>(workspace);
        await workspaceCacheReposotory.SetAsync(workspace.Id.ToString(), workspaceDto);

        if (noUsersInWorkspace)
            return;

        var @event = new UserAddedToWorkspaceAppEvent
        {
            UserId = newUser.OuterId,
            WorkspaceId = workspace.Id,
            UsersId = workspace.Users.Select(x => x.OuterId)
        };
        await applicationEventSerivce.PublishThroughEventBus(@event);
    }

    /// <inheritdoc/>
    public async Task<Guid> CreateWorkspaceAsync(WorkspaceDto workspaceDto)
    {
        var workspaceEntity = mapper.Map<WorkspaceEntity>(workspaceDto);
        var id = await unitOfWork.Workspaces.CreateAsync(workspaceEntity);
        await unitOfWork.Workspaces.SaveChangesAsync();
        workspaceDto.Id = id;
        await workspaceCacheReposotory.SetAsync(id.ToString(), workspaceDto);
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
        var cachedEntity = await workspaceCacheReposotory.GetAsync(workspaceId.ToString());
        if(cachedEntity != null)
            return cachedEntity;
        var entity = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Users).Include(w => w.Servers).FirstAsync();
        var workspaceDto = mapper.Map<WorkspaceDto>(entity);
        return workspaceDto;

    }

    /// <inheritdoc/>
    public async Task<IEnumerable<WorkspaceDto>> GetAllWorkspacesAsync()
    {
        var workspaces = await unitOfWork.Workspaces.GetAll().Include(w => w.Users).Include(w => w.Servers).ToListAsync();
        var workspacesDto = workspaces.Select(w => mapper.Map<WorkspaceDto>(w)).ToList();
        return workspacesDto;
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
        var workspcaeDtoFromCache = await workspaceCacheReposotory.GetAsync(workspaceId.ToString());
        if(workspcaeDtoFromCache != null)
            return workspcaeDtoFromCache.Servers;

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
        var workspcaeDtoFromCache = await workspaceCacheReposotory.GetAsync(workspaceId.ToString());
        if(workspcaeDtoFromCache != null)
            return workspcaeDtoFromCache.Users;

        var workspace = await unitOfWork.Workspaces.GetAll()
            .Where(w => w.Id == workspaceId)
            .Include(w => w.Users)
            .FirstAsync();
        var users = workspace.Users.Select(s => s.OuterId);
        return users;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Removes server from workspace and sends an event if there are users in this workspace
    /// </summary>
    /// <param name="workspaceId">Workspace identifer</param>
    /// <param name="serverId">Server identifer</param>
    /// <returns></returns>
    public async Task<bool> RemoveServerFromWorkspaceAsync(Guid workspaceId, Guid serverId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Servers).Include(x => x.Users).FirstAsync();
        var server = workspace.Servers.FirstOrDefault(x => x.OuterId == serverId);
        if (server == null)
            return false;
        await unitOfWork.Servers.DeleteAsync(server.Id);
        await unitOfWork.Servers.SaveChangesAsync();

        var workspaceDto = mapper.Map<WorkspaceDto>(workspace);
        await workspaceCacheReposotory.SetAsync(workspace.Id.ToString(), workspaceDto);

        if (!workspace.Users.Any())
            return true;

        var @event = new ServerRemovedFromWorkspaceAppEvent
        {
            ServerId = server.OuterId,
            WorkspaceId = workspace.Id,
            UsersId = workspace.Users.Select(x => x.OuterId)
        };
        await applicationEventSerivce.PublishThroughEventBus(@event);

        return true;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Removed user from workspace and sends an event if there is anyone left in workspace
    /// </summary>
    /// <param name="workspaceId">Workspace identifier</param>
    /// <param name="userId">User identifier</param>
    /// <returns></returns>
    public async Task<bool> RemoveUserFromWorkspaceAsync(Guid workspaceId, Guid userId)
    {
        var workspace = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Users).FirstAsync();
        var user = workspace.Users.FirstOrDefault(x => x.OuterId == userId);
        if (user == null)
            return false;
        await unitOfWork.Users.DeleteAsync(user.Id);
        await unitOfWork.Servers.SaveChangesAsync();

        var workspaceDto = mapper.Map<WorkspaceDto>(workspace);
        await workspaceCacheReposotory.SetAsync(workspace.Id.ToString(), workspaceDto);

        if (!workspace.Users.Any())
            return true;

        var @event = new UserRemovedFromWorkspaceAppEvent
        {
            UserId = user.OuterId,
            WorkspaceId = workspace.Id,
            UsersId = workspace.Users.Select(x => x.OuterId)
        };
        await applicationEventSerivce.PublishThroughEventBus(@event);

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateWorkspaceAsync(Guid workspaceId, WorkspaceDto workspaceDto)
    {
        // Update entity in database
        var workspace = await unitOfWork.Workspaces.GetAll().Where(w => w.Id == workspaceId).Include(w => w.Servers).Include(x => x.Users).FirstAsync();
        workspace.Description = workspaceDto.Description;
        workspace.Name = workspaceDto.Name;
        unitOfWork.Workspaces.Update(workspace);
        //await unitOfWork.Workspaces.SaveChangesAsync();

        var workspaceUsersOuterIds = workspace.Users.Select(user => user.OuterId);
        var usersToDelete = workspace.Users.Where(user => !workspaceDto.Users.Contains(user.OuterId)).ToList();
        var usersToAdd = workspaceDto.Users.Where(userOuterId => !workspaceUsersOuterIds.Contains(userOuterId))
            .Select(userOuterId => new User(){OuterId = userOuterId, Workspace = workspace}).ToList();

        if(usersToDelete != null && usersToDelete.Count() > 0)
            unitOfWork.Users.DeleteRange(usersToDelete);
        if(usersToAdd != null && usersToAdd.Count() > 0)
            await unitOfWork.Users.CreateRangeAsync(usersToAdd);

        var workspaceServersOuterIds = workspace.Servers.Select(s => s.OuterId);
        var serversToDelete = workspace.Servers.Where(s => !workspaceDto.Servers.Contains(s.OuterId)).ToList();
        var serversToAdd = workspaceDto.Servers.Where(serverId => !workspaceServersOuterIds.Contains(serverId))
            .Select(serverOuterId => new Server(){OuterId = serverOuterId, Workspace = workspace}).ToList();

        if(serversToDelete != null && serversToDelete.Count() > 0)
            unitOfWork.Servers.DeleteRange(serversToDelete);
        if(serversToAdd != null && serversToAdd.Count() > 0)
            await unitOfWork.Servers.CreateRangeAsync(serversToAdd);

        await unitOfWork.Workspaces.SaveChangesAsync();

        // await ReplaceEntities(workspace.Users, workspaceDto.Users, unitOfWork.Users, workspace);
        // await ReplaceEntities(workspace.Servers, workspaceDto.Servers, unitOfWork.Servers, workspace);

        // Update entity in cashe
        // var workspaceToCache = mapper.Map<WorkspaceDto>(workspace);
        // await workspaceCacheReposotory.SetAsync(workspace.Id.ToString(), workspaceToCache);

        if(!workspace.Users.Any())
            return true;
        
        // Create and publish events to message bus
        var userAddedToWorkspaceEvents = usersToAdd.Select(user => UserAddedToWorkspaceAppEvent.FromModels(user, workspace));
        await applicationEventSerivce.PublishManyThroughEventBus(userAddedToWorkspaceEvents);

        var userRemovedFromWorkspaceEvents = usersToDelete.Select(user => UserRemovedFromWorkspaceAppEvent.FromModels(user, workspace));
        await applicationEventSerivce.PublishManyThroughEventBus(userRemovedFromWorkspaceEvents);

        var serverAddedToWorkspaceEvents = serversToAdd.Select(server => ServerAddedToWorkspaceAppEvent.FromModels(server, workspace));
        await applicationEventSerivce.PublishManyThroughEventBus(serverAddedToWorkspaceEvents);

        var ServerRemovedFromWorkspaceAppEvents = serversToDelete.Select(server => ServerRemovedFromWorkspaceAppEvent.FromModels(server, workspace));
        await applicationEventSerivce.PublishManyThroughEventBus(ServerRemovedFromWorkspaceAppEvents);

        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> WorkspaceExists(Guid id)
        => (await unitOfWork.Workspaces.GetAsync(id)) != null;

}