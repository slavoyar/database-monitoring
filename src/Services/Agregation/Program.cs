using System.Reflection;
using Agregation.Domain.Intefaces;
using Agregation.Infrastructure.DataAccess;
using Agregation.Infrastructure.Services.Implementations;
using Hangfire;
using Hangfire.PostgreSql;
using MIAUDataBase;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
AddCustomLogging(builder);

/*
 This method adds mappers, database connection, repositories and set services
 (set service is a service for working with repositories)
*/
builder.Services.AddServices(configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AgreregationApi",
        Description = "ASP.NET Core Web Api for Agregation service"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<IHangFireJobService, HangFireJobService>();
var connectionString = builder.Configuration.GetConnectionString("Postgre");
builder.Services.AddHangfire(x =>
{
    x.UsePostgreSqlStorage(connectionString, new PostgreSqlStorageOptions
    {
        InvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.FromMilliseconds(200),
        DistributedLockTimeout = TimeSpan.FromMinutes(1),
    });
});
builder.Services.AddHangfireServer();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "front" || new Uri(origin).Host == "localhost")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//--------------------------------------------------------------------------------------------------------------

var app = builder.Build();

var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<ApplicationContext>();
var db = dbContext?.Database;

for (int i = 0; i < 10; i++)
{
    if (db != null && db.CanConnect())
    {
        await db.MigrateAsync();
        break;
    };

    Task.Delay(TimeSpan.FromSeconds(3)).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
            {
                new  HangfireAuthorizationFilter()
            }
});

RecurringJobManager recurringJobManager = new();
var scopedHangFireService =
            scope.ServiceProvider
                .GetRequiredService<IHangFireJobService>();
var jobid = "AggLoopedLogs";
recurringJobManager.AddOrUpdate(jobid, () => scopedHangFireService.ReccuringJob(), Cron.Minutely);
recurringJobManager.Trigger(jobid);

app.MapHub<ServerStateHub>("/serversState");


app.Run();

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
            IndexFormat = "aggregation-service-{0:yyyy.MM.dd}",
            AutoRegisterTemplate = true,
            EmitEventFailure = EmitEventFailureHandling.ThrowException | EmitEventFailureHandling.RaiseCallback | EmitEventFailureHandling.WriteToSelfLog
        });
    });
}
