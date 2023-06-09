using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticiaMAUI.Models
{
   public class Noticia
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public string Autor { get; set; } = null!;

        public string Contenido { get; set; } = null!;

        public string Imagen { get; set; } = null!;

        public DateTime Fecha { get; set; }
        public Noticia()
        {
            Fecha = DateTime.Now;
        }
    }

   
}
