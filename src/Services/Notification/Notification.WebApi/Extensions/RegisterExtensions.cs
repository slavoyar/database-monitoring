namespace DatabaseMonitoring.Services.Notification.WebApi.Extensions;

public static class RegisterExtensions
{
    public static IServiceCollection AddCustomHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("WorkspaceService", client => {
            client.BaseAddress = new Uri(configuration["WorkspaceConfiguration:BaseAddress"]);
        });

        return services;
    }
}