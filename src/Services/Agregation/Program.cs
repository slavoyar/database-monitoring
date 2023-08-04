using Hangfire;
using Hangfire.PostgreSql;
using Agregation.Infrastructure.DataAccess;
using MIAUDataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Agregation.Infrastructure.Services.Implementations;
using Agregation.Domain.Intefaces;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

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

app.UseHttpsRedirection();

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