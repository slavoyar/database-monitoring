
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
}