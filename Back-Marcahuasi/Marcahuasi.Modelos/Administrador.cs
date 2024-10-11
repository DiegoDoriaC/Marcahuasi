using System;
using System.Collections.Generic;

namespace Marcahuasi.Modelos;

public partial class Administrador
{
    public int IdAdministrado { get; set; }

    public string Nombre { get; set; } = null!;

    public string NumeroTelefono { get; set; } = null!;

    public string Contraceña { get; set; } = null!;

    public DateTime? FechaModificacion { get; }

    public Administrador()
    {
        FechaModificacion = DateTime.Now;
    }

}
