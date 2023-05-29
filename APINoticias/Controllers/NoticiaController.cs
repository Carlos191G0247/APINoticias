using APINoticias.Models;
using APINoticias.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APINoticias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : ControllerBase
    {
        private readonly Sistem21DailyBugleContext context;
        Repository<Noticia> repoNoticia;


        public NoticiaController(Sistem21DailyBugleContext context)
        {
            this.context = context;
            repoNoticia =  new Repository<Noticia>(context);
        }
        [HttpGet]
        public IActionResult Get() 
        {
            var entidad = repoNoticia.Get();
            return Ok(entidad);
        }
        [HttpPost]
        public IActionResult Post(Noticia noticia)
        {
            if (noticia != null) 
            {
                repoNoticia.Insert(noticia);
            }
            return Ok(noticia);
        }
        [HttpPut]
        public IActionResult Put(Noticia noticia) 
        {
            var entidad = repoNoticia.Get(noticia.Id);
            if (entidad != null) 
            {
                entidad.Titulo = noticia.Titulo;
                entidad.Autor = noticia.Autor;
                entidad.Contenido = noticia.Contenido;
                entidad.Imagen = noticia.Imagen;
                repoNoticia.Update(entidad);
            }
           
            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entidad = repoNoticia.Get(id);
            if (entidad==null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
