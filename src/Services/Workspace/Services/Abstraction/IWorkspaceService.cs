namespace DatabaseMonitoring.Services.Workspace.Services.Abstraction;

public interface IWorkspaceService
{

    Task<WorkspaceDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(WorkspaceDto workspaceDto);
    Task<bool> DeleteAsync(Guid id);
}