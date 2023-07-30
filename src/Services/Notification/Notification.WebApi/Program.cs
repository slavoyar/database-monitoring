var builder = WebApplication.CreateBuilder(args);
AddCustomLogging(builder);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.ConfigureSwaggerGen();

// Add services to the container.
builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection(nameof(MailConfiguration)));
builder.Services.Configure<MongoDbConfiguration>(builder.Configuration.GetSection(nameof(MongoDbConfiguration)));

builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMailService, MailService>();

builder.Services.RegisterRabbitMq(builder.Configuration);

var app = builder.Build();

app.Services.EventBusSubscribeToEvents();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();


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
            IndexFormat = "notification-service-{0:yyyy.MM.dd}",
            AutoRegisterTemplate = true,
            EmitEventFailure = EmitEventFailureHandling.ThrowException | EmitEventFailureHandling.RaiseCallback | EmitEventFailureHandling.WriteToSelfLog
        });
    });
}