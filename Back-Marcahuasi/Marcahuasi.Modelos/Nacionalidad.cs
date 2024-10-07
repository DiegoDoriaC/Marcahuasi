using System;
using System.Collections.Generic;

namespace Marcahuasi.Modelos;

public partial class Nacionalidad
{
    public int IdNacionalidad { get; set; }

    public string Pais { get; set; } = null!;

    public string CodigoIso { get; set; } = null!;

    public string UrlImagen { get; set; } = null!;

    public virtual ICollection<Turista> Turista { get; set; } = new List<Turista>();
}
