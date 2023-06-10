

namespace DatabaseMonitoring.Services.Workspace.Mapping;

public class WorkspaceMappingProfile : Profile
{
    public WorkspaceMappingProfile()
    {
        CreateMap<WorkspaceEntity, WorkspaceDto>().ReverseMap();

        CreateMap<WorkspaceDto, GetWorkspaceResponce>();
        CreateMap<CreateWorkspaceRequest, WorkspaceDto>();

    }
}