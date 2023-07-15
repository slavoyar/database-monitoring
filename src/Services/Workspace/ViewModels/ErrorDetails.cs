

namespace DatabaseMonitoring.Services.Workspace.ViewModels;

/// <summary>
/// Error details view model
/// </summary>
public class ErrorDetails
{

    /// <summary>
    /// Error id
    /// </summary>
    public Guid ErrorId { get; set; }

    /// <summary>
    /// Error status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Serialize object on ToString method
    /// </summary>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}