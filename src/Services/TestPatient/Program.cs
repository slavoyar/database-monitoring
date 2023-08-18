using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestPatient.Data;
using TestPatient.Interfaces;
using TestPatient.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestPatientApplication", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddDbContext<HangfireContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IHangFireJobService, HangFireJobService>();
builder.Services.AddHangfire(x =>
{
    x.UsePostgreSqlStorage(connectionString);
});
builder.Services.AddHangfireServer();

//--------------------------------------------------------------------------------------------------------------

var app = builder.Build();

var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<HangfireContext>();
var db = dbContext?.Database;

for (int i = 0; i < 10; i++)
{
    if (db != null && db.CanConnect())
    {
        db.Migrate();
        break;
    };

    Task.Delay(TimeSpan.FromSeconds(3)).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestPatientApplication v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[]
        {
            new  HangfireAuthorizationFilter("admin")
        }
});

RecurringJobManager recurringJobManager = new();
var scopedHangFireService =
            scope.ServiceProvider
                .GetRequiredService<IHangFireJobService>();
var jobid = "TestPatientLoopedLogs";
recurringJobManager.AddOrUpdate(jobid, () => scopedHangFireService.ReccuringJob(), Cron.Minutely);
recurringJobManager.Trigger(jobid);

app.Run();