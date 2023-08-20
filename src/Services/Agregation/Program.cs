using System.Reflection;
using Agregation.Domain.Intefaces;
using Agregation.Infrastructure.DataAccess;
using Agregation.Infrastructure.Services.Implementations;
using Hangfire;
using Hangfire.PostgreSql;
using MIAUDataBase;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
           new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
             Array.Empty<string>()
            }
    });
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
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "nginx" || new Uri(origin).Host == "localhost")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
    options.AddPolicy("SignalRPolicy",
    builder =>
    {
        builder
        .WithOrigins("http://localhost:8080", "http://localhost")
        .WithMethods("GET", "POST")
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

//--- Add Jwt
var jwtSecret = configuration["JWT:Secret"];

if (jwtSecret != null)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true, // Check if token expired = no access
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
            };
        });
}

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

app.UseCors("SignalRPolicy");

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

app.MapHub<ServerStateHub>("/serverState");

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