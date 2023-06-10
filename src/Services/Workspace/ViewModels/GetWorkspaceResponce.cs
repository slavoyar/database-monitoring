namespace DatabaseMonitoring.Services.Workspace.ViewModels;

public class GetWorkspaceResponce
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Server> Servers { get; set; }
    public DateTime CreationDate { get; set; }
}