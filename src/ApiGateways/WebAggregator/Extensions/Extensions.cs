using Services.Common;

internal static class Extensions
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
        return services;
    }
}