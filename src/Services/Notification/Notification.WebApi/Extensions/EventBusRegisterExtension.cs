

namespace DatabaseMonitoring.Services.Notification.WebApi.Extensions;

public static class EventBusRegisterExtension
{
    public static IServiceCollection RegisterRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        services.AddSingleton<IRabbitMQPersistentConnection>(sp => {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory(){
                        HostName = configuration["EventBusConfiguration:Connection"] ,
                        DispatchConsumersAsync = true
                        };
                var retryCount = 5;
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => {
            var clientName = configuration["EventBusConfiguration:SubscriptionClientName"];
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var subManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var persistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var retryCount = 5;

            return new EventBusRabbitMQ(persistentConnection,logger,sp,subManager,clientName,retryCount);
        });

        services.AddTransient<ServerRemovedFromWorkspaceAppEventHandler>();
        services.AddTransient<ServerAddedToWorkspaceAppEventHandler>();
        services.AddTransient<UserAddedToWorkspaceAppEventHandler>();
        services.AddTransient<UserRemovedFromWorkspaceAppEventHandler>();

        return services;
    }

    public static IServiceProvider EventBusSubscribeToEvents(this IServiceProvider services)
    {
        var eventbus = services.GetRequiredService<IEventBus>();
        eventbus.Subscribe<ServerRemovedFromWorkspaceAppEvent, ServerRemovedFromWorkspaceAppEventHandler>();
        eventbus.Subscribe<ServerAddedToWorkspaceAppEvent, ServerAddedToWorkspaceAppEventHandler>();
        eventbus.Subscribe<UserAddedToWorkspaceAppEvent, UserAddedToWorkspaceAppEventHandler>();
        eventbus.Subscribe<UserRemovedFromWorkspaceAppEvent, UserRemovedFromWorkspaceAppEventHandler>();
        return services;
    }
}