using System;
using System.Collections.Generic;

namespace Marcahuasi.Modelos;

public partial class TarifaPago
{
    public int IdTarifa { get; set; }

    public string NombreTarifa { get; set; } = null!;

    public decimal MontoTarifa { get; set; }

    public virtual ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
}
