using MIAUDataBase;

var builder = WebApplication.CreateBuilder(args);

/**
* Устанавливает мапперы, базу данных, репозитории и сервисы множеств
* (в смысле коллекции элементов дто, получаемых из бд)
**/
builder.Services.AddServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//TODO: После переноса не совпадают пространства имён с их местоположением.