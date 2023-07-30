using Agregation.Infrastructure.DataAccess;
using MIAUDataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
AddCustomLogging(builder);

/*
 This method adds mappers, database connection, repositories and set services
 (set service is a service for working with repositories)
*/
builder.Services.AddServices(builder.Configuration);

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

var app = builder.Build();

await UpdateDatabaseAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task UpdateDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    await context.Database.MigrateAsync();
}

void AddCustomLogging(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((context, services, configuration) => {
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