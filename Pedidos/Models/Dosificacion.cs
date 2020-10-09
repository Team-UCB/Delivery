using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Dosificacion
    {
        public Dosificacion()
        {
            Facturas = new HashSet<Factura>();
        }

        public long Id { get; set; }
        public long NroAutorizacion { get; set; }
        public long NroFacturaActual { get; set; }
        public string Llave { get; set; }
        public DateTime FechaLimiteEmision { get; set; }
        public string Leyenda { get; set; }
        public DateTime FechaActivacion { get; set; }
        public long Activa { get; set; }
        public string ActividadPrincipal { get; set; }
        public string ActividadSecundaria { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
