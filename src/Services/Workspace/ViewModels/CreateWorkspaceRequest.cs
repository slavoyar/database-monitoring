namespace DatabaseMonitoring.Services.Workspace.ViewModels;

public class CreateWorkspaceRequest
{
    [Required]
    public string Name { get; set; }
    [MaxLength(250)]
    public string Description { get; set; }
}