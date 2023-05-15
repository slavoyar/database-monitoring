using DatabaseMonitoring.Ping_Connect.CustomEvents;
using DatabaseMonitoring.Ping_Connect.CustomEventsHandlers;
using DatabaseMonitoring.BuildingBlocks.EventBus;
using DatabaseMonitoring.BuildingBlocks.EventBus.Abstractions;
using DatabaseMonitoring.BuildingBlocks.EventBusRabbitMQ;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterRabbitMQ(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.ConfigureRabbitMQSubscription();

app.Run();

public static class Extensions
{
    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
            var factory = new ConnectionFactory()
            {
                HostName = configuration["EventBusConnection"],
                DispatchConsumersAsync = true
            };
            const int retryCount = 5;
            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
        {
            var clientName = configuration["SubscriptionClientName"];
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
            var subManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var persistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            const int retryCount = 5;

            return new EventBusRabbitMQ(persistentConnection, logger, sp, subManager, clientName, retryCount);
        });

        services.AddTransient<PingToWebEventsHandlers>();
        services.AddTransient<ConnectToDatabaseEventsHandlers>();

        return services;
    }

    public static IApplicationBuilder ConfigureRabbitMQSubscription(this IApplicationBuilder app)
    {
        var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        eventBus.Subscribe<PingToWebEvents, PingToWebEventsHandlers>();
        eventBus.Subscribe<ConnectToDatabaseEvents, ConnectToDatabaseEventsHandlers>();
        return app;

    }
}