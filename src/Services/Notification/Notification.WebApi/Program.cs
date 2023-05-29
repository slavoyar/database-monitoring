
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmailNotificationDb"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailConfiguration>(builder.Configuration.GetSection(nameof(MailConfiguration)));
builder.Services.Configure<WorkspaceConfiguration>(builder.Configuration.GetSection(nameof(WorkspaceConfiguration)));

builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IRepository<MailEntity>, EfRepository<MailEntity>>();
builder.Services.AddScoped<IRepository<ErrorSending>, EfRepository<ErrorSending>>();

var app = builder.Build();

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
