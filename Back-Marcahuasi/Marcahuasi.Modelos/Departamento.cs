using System;
using System.Collections.Generic;

namespace Marcahuasi.Modelos;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public string? NombreDepartamento { get; set; }

    public virtual ICollection<Turista> Turista { get; set; } = new List<Turista>();
}
