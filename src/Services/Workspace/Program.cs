using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

var builder = WebApplication.CreateBuilder(args);
AddCustomLogging(builder);

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCustomAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "front" || new Uri(origin).Host == "localhost")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

await UpdateDatabaseAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

async Task UpdateDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await SeedData(context);
    await context.Database.MigrateAsync();
}

void AddCustomLogging(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ELASTICSEARCH_URL"]))
        {
            FailureCallback = e =>
            {
                Console.WriteLine("Unable to submit event " + e.Exception);
            },
            FailureSink = new FileSink("./failures.txt", new JsonFormatter(), null),
            TypeName = null,
            IndexFormat = "workspace-service-{0:yyyy.MM.dd}",
            AutoRegisterTemplate = true,
            EmitEventFailure = EmitEventFailureHandling.ThrowException | EmitEventFailureHandling.RaiseCallback | EmitEventFailureHandling.WriteToSelfLog
        });
    });
}

async Task SeedData(AppDbContext context)
{
    var workspaceId = new Guid("bc86a603-9a19-43ed-9bde-c5d3fc7f02c3");
    var searchedWorkspace = context.Workspaces.Find(workspaceId);

    if (searchedWorkspace != null) return;

    var defaultWorkspace = new WorkspaceEntity()
    {
        Id = workspaceId,
        Name = "Workspace Analytics",
        Description = "Department Analytics - Visual Reports System",
        Users = new List<User>(),
        Servers = new List<Server>()
    };

    var defaultUserAdmin = new User()
    {
        Id = new Guid("314250b2-2de8-4b05-838f-239cdf7c369c"),
        OuterId = new Guid("7499844d-efea-4494-9612-39138922c9db"),
        Workspace = defaultWorkspace
    };

    var defaultFirstTestPatientServer = new Server()
    {
        Id = new Guid("13b70df2-3560-4451-a456-1b7d5323d64a"),
        OuterId = new Guid("d69cd87f-1f08-4b12-af16-980b003cdc5f"),
        Workspace = defaultWorkspace
    };

    var defaultSecondTestPatientServer = new Server()
    {
        Id = new Guid("e6c5372d-dca7-45b1-b4ec-e5985349c680"),
        OuterId = new Guid("d13920a2-4961-43cc-bd22-12187b19f512"),
        Workspace = defaultWorkspace
    };

    var defaultThirdTestPatient = new Server()
    {
        Id = new Guid("d481ad57-3983-4ab0-95de-bd357cb46aaa"),
        OuterId = new Guid("8d8a6029-676a-4e09-91c5-32c56602f67f"),
        Workspace = defaultWorkspace
    };

    defaultWorkspace.Users.Add(defaultUserAdmin);
    defaultWorkspace.Servers.Add(defaultFirstTestPatientServer);
    defaultWorkspace.Servers.Add(defaultSecondTestPatientServer);
    defaultWorkspace.Servers.Add(defaultThirdTestPatient);

    context.Users.Add(defaultUserAdmin);
    context.Servers.AddRange(defaultFirstTestPatientServer, defaultSecondTestPatientServer, defaultThirdTestPatient);
    context.Workspaces.Add(defaultWorkspace);
    await context.SaveChangesAsync();
}