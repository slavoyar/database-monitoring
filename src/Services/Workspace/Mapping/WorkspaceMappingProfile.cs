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
        CreateMap<WorkspaceEntity, WorkspaceDto>().ReverseMap();

        CreateMap<WorkspaceDto, GetWorkspaceResponce>();
        CreateMap<UpsertWorkspaceRequest, WorkspaceDto>();

    }
}