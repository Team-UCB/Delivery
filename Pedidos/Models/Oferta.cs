using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Oferta
    {
        public long Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PrecioOferta { get; set; }
        public long IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
    }
}
