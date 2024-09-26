using System;
using System.Collections.Generic;

namespace Marcahuasi.Modelos;

public partial class Turista
{
    public int IdTurista { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string NumeroDocumento { get; set; } = null!;

    public int? IdNacionalidad { get; set; }

    public int? IdDepartamento { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Nacionalidad? IdNacionalidadNavigation { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
