using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Dosificacion
    {
        public Dosificacion()
        {
            Factura = new HashSet<Factura>();
        }

        public long Id { get; set; }
        public long NroAutorizacion { get; set; }
        public long NroFacturaActual { get; set; }
        public string Llave { get; set; }
        public DateTime FechaLimiteEmision { get; set; }
        public string Leyenda { get; set; }
        public DateTime FechaActivacion { get; set; }
        public long Activa { get; set; }

        public virtual ICollection<Factura> Factura { get; set; }
    }
}
