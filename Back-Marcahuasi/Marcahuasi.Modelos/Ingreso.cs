using System;
using System.Collections.Generic;

namespace Marcahuasi.Modelos;

public partial class Ingreso
{
    public int IdIngreso { get; set; }

    public int? IdTurista { get; set; }

    public int? IdTarifa { get; set; }

    public DateTime? FechaIngreso { get; }

    public bool Modificado { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? Observacion { get; set; }

    public string UserRegister { get; set; } = null!;

    public virtual TarifaPago? IdTarifaNavigation { get; set; }

    public virtual Turista? IdTuristaNavigation { get; set; }
}
