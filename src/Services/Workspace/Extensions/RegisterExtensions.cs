

namespace DatabaseMonitoring.Services.Workspace.Extensions;

/// <summary>
/// Service register extensions
/// </summary>
public static class RegisterExtensions
{

    /// <summary>
    /// Registerin all service for application to work
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationConfigurations(configuration);
        services.AddDatabaseContext(configuration);
        services.RegisterRabbitMQ(configuration);
        services.RegisterServices();
        services.RegisterUnitOfWork();
        services.RegisterRepositories();
        services.ConfigureSwaggerGen();
        services.AddAutomapperProfiles();

        return services;
    }


    /// <summary>
    /// Add database context
    /// </summary>
    private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => {
            options.UseNpgsql(configuration.GetSection("PostgreConfiguration")["ConnectionString"], o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery));
        });
        return services;
    }
    
  
    /// <summary>
    /// Register options
    /// </summary>
    private static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

   
    /// <summary>
    /// Services registration
    /// </summary>
    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IWorkspaceService, WorkspaceService>();
        return services;
    }

   
    /// <summary>
    /// Unit of work registration
    /// </summary>
    private static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }

   
    /// <summary>
    /// Repository registration
    /// </summary>
    private static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRepository<WorkspaceEntity>, EfRepository<WorkspaceEntity>>();
        services.AddTransient<IRepository<User>, EfRepository<User>>();
        services.AddTransient<IRepository<Server>, EfRepository<Server>>();
        return services;
    }

   
    /// <summary>
    /// Add automapper profiles
    /// </summary>
    private static IServiceCollection AddAutomapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(new[]{typeof(WorkspaceMappingProfile)});
        return services;
    }

   
    /// <summary>
    /// Swagger gen configuration method
    /// </summary>
    private static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "WorkspaceApi",
                Description = "ASP.NET Core Web Api for managing workspaces"
            });
            
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        return services;
    }


    /// <summary>
    /// RabbitMQ event bus configuration method
    /// </summary>
    public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
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

        services.AddTransient<IApplicationEventService, ApplicationEventService>();

        return services;
    }

}