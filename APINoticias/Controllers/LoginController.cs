using APINoticias.Models;
using APINoticias.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APINoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly Sistem21DailyBugleContext context;
        private readonly Repository<Usuario> usuarioRepository;

        public LoginController(Sistem21DailyBugleContext context)
        {
            this.context = context;
            usuarioRepository = new Repository<Usuario>(context);
        }
        [HttpPost]
        public IActionResult Login(Usuario datos)
        {
            var depto = usuarioRepository.Get().SingleOrDefault(x => x.Usuario1 == datos.Usuario1 && x.Contraseña == datos.Contraseña);
            if (depto == null)
            {
                return Unauthorized("Contraseña o Correo electronico incorrecto.");
            }
            else
            {
                //1. Crear los claims
                List<Claim> claims = new List<Claim>
                {
                    new Claim("Id", depto.Id.ToString()),
                    new Claim("Usuario", depto.Usuario1),
                    new Claim("Nombre", depto.Nombre),

                    
                };

                //2. Crear el token
                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = "https://dailybugle.sistemas19.com",
                    Audience = "mauinoticias",
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddHours(0.5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("NoticaismauiKeyMoviles191G")),
                        SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme)
                };

                //3. Regresar el token
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(tokenDescriptor);

                return Ok(handler.WriteToken(token));
            }
        }
    }

}
