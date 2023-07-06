
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

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