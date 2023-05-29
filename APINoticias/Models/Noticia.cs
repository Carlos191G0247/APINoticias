using System;
using System.Collections.Generic;

namespace APINoticias.Models;

public partial class Noticia
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Contenido { get; set; } = null!;

    public string Imagen { get; set; } = null!;

    public DateOnly Fecha { get; set; }
}
