using APINoticias.Models;
using APINoticias.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APINoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly Sistem21DailyBugleContext context;
        Repository<Usuario> repoUsuario;


        public UsuarioController(Sistem21DailyBugleContext context)
        {
            this.context = context;
            repoUsuario = new Repository<Usuario>(context);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var entidad = repoUsuario.Get();
            return Ok(entidad);
        }
        [HttpGet("adminin")]
        public IActionResult GetIdExcept()
        {
            var usuarios = repoUsuario.Get().Where(x=>x.Id !=1);
            return Ok(usuarios);
        }
        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            if (usuario != null)
            {
               repoUsuario.Insert(usuario);
            }
            return Ok(usuario);
        }
        [HttpPut]
        public IActionResult Put(Usuario usuario)
        {
            var entidad = repoUsuario.Get(usuario.Id);
            if (entidad != null)
            {
                entidad.Usuario1 = usuario.Usuario1;
                entidad.Contraseña = usuario.Contraseña;
                entidad.Nombre = usuario.Nombre;
            
                repoUsuario.Update(entidad);
            }

            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entidad = repoUsuario.Get(id);
            if (entidad == null)
            {
                return NotFound();
            }
            repoUsuario.Delete(entidad);
            return Ok();
        }
    }
}
