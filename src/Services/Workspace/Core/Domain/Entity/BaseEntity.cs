namespace DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
}