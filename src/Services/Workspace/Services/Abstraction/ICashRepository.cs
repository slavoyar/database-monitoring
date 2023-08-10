namespace DatabaseMonitoring.Services.Workspace.Services.Abstraction;

/// <summary>
/// Interface for working with cash
/// </summary>
public interface ICasheRepository<T>
{
    /// <summary>
    /// Get value from cash
    /// </summary>
    /// <param name="key">Cash key</param>
    /// <returns>Serialized cashe value</returns>
    Task<T> GetAsync(string key);

    /// <summary>
    /// Set value to cash
    /// </summary>
    /// <param name="key">Cash key</param>
    /// <param name="value">Value</param>
    /// <returns>Serialized cashe value</returns>
    Task SetAsync(string key, T value);

}