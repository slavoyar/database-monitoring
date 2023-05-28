var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

app.Run();
