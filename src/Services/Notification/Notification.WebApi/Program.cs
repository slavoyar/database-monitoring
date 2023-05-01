using DatabaseMonitoring.Services.Notification.Core.Interfaces;
using DatabaseMonitoring.Services.Notification.Infrastructure.Services;
using DatabaseMonitoring.Services.Notification.Infrastructure.Models;
using DatabaseMonitoring.Services.Notification.Core.Models;
using DatabaseMonitoring.Services.Notification.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection(SmtpSettings.Smtp));
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddSingleton<IRepository<EmailMessage>, EfRepository<EmailMessage>>();

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
