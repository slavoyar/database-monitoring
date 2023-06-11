namespace DatabaseMonitoring.Services.Workspace.Core.Domain.Entity;

/// <summary>
/// Base abstract model
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identifier
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Date of creation
    /// </summary>
    public DateTime CreationDate { get; set; }
}