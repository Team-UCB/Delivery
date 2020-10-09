using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class DetalleFactura
    {
        public long Id { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public long IdDetallePedido { get; set; }
        public long IdFactura { get; set; }

        public virtual DetallePedido IdDetallePedidoNavigation { get; set; }
        public virtual Factura IdFacturaNavigation { get; set; }
    }
}
