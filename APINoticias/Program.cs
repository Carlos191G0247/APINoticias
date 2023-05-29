using APINoticias.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string? cadena = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddDbContext<Sistem21DailyBugleContext>(ob => ob.UseMySql(cadena, ServerVersion.AutoDetect(cadena)));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
