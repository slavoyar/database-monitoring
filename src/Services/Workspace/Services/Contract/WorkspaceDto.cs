namespace DatabaseMonitoring.Services.Workspace.Services.Contract;

public class WorkspaceDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Server> Servers { get; set; }
    public DateTime CreationDate { get; set; }
}