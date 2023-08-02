namespace DatabaseMonitoring.Services.Workspace.Mapping;


/// <summary>
/// Workspace mapping profiles
/// </summary>
public class WorkspaceMappingProfile : Profile
{
    /// <summary>
    /// Contructor
    /// </summary>
    public WorkspaceMappingProfile()
    {
        CreateMap<WorkspaceEntity, WorkspaceDto>()
            .ForMember(w => w.Servers, options => options.MapFrom(src => src.Servers.Select(s => s.OuterId)))
            .ForMember(w => w.Users, options => options.MapFrom(src => src.Users.Select(s => s.OuterId)));

        CreateMap<WorkspaceDto, WorkspaceEntity>()
            .ForMember(w => w.Servers, options => options.MapFrom(src => src.Servers.Select(s => new Server() { OuterId = s })))
            .ForMember(w => w.Users, options => options.MapFrom(src => src.Users.Select(u => new Server() { OuterId = u })));

        CreateMap<WorkspaceDto, GetWorkspaceResponse>();
        CreateMap<UpsertWorkspaceRequest, WorkspaceDto>();
    }
}