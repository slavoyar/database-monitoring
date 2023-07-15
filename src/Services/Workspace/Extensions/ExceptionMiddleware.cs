namespace DatabaseMonitoring.Services.Workspace.Extensions;

/// <summary>
/// Middleware for exception handling
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> logger;

    /// <summary>
    /// Ctor
    /// </summary>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        this.logger = logger;
    }

    /// <summary>
    /// Middleware invoke method
    /// </summary>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var exceptionId = Guid.NewGuid();
            logger.LogError("Id: {Id}. Unhandled exception occured: {ex}", exceptionId, ex);
            await HandleExceptionAsync(httpContext, ex, exceptionId);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception, Guid exceptionId)
    {
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            ErrorId = exceptionId,
            Message = "Ooops, something went wrong"
        }.ToString());
    }
}