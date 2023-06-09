namespace DatabaseMonitoring.Services.Notification.WebApi.Extensions;

public static class EventBusExtensions
{
    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        services.AddSingleton<IRabbitMQPersistentConnection>(sp => {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory(){
                        HostName = configuration["EventBusConnection"] ,
                        DispatchConsumersAsync = true
                        };
                var retryCount = 5;
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp => {
            var clientName = configuration["SubscriptionClientName"];
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var subManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var persistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var retryCount = 5;

            return new EventBusRabbitMQ(persistentConnection,logger,sp,subManager,clientName,retryCount);
        });
    
        services.AddTransient<NewNotificationCreatedEventHandler>();

        return services;
    }

    public static IApplicationBuilder ConfigureRabbitMQSubscriptions(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<NewNotificationCreatedEvent, NewNotificationCreatedEventHandler>();
        return app;

    }
}