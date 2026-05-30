using car.Api.Data;
using car.Api.Repositories;
using car.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

// Inyectamos las dependencias de los servicios y repositorios,
// para poder acceder a las instancias unicas de cada clase,
// y poder acceder a los metodos de cada clase desde cualquier punto del proyecto.
builder.Services.AddScoped<IMarcasAutosRepository, MarcasAutosRepository>();
builder.Services.AddScoped<IMarcasAutosService, MarcasAutosService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
