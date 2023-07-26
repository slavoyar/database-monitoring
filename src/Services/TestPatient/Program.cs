using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.OpenApi.Models;
using TestPatient.Data;
using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

using ( var scope = app.Services.CreateScope() )
{
    var dbContext = scope.ServiceProvider.GetService<HangfireContext>();
    try
    {
        dbContext?.Database.Migrate();
    }
    catch ( Exception )
    {
    }
}

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestPatientApplication v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();