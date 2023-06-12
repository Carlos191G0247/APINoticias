using APINoticias.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string? cadena = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddDbContext<Sistem21DailyBugleContext>(ob => ob.UseMySql(cadena, ServerVersion.AutoDetect(cadena)));


string? issuer = "https://dailybugle.sistemas19.com";
string? audience = "mauinoticias"; 
var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("NoticaismauiKeyMoviles191G"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwt =>
    {
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = secret,
            ValidateAudience = true,
            ValidateIssuer = true
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
