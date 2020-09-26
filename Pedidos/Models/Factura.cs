using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Factura
    {
        public Factura()
        {
            DetalleFactura = new HashSet<DetalleFactura>();
        }

        public long Id { get; set; }
        public long NroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public string Estado { get; set; }
        public string CodigoControl { get; set; }
        public string Observaciones { get; set; }
        public long IdDosificacion { get; set; }
        public long IdPedido { get; set; }

        public virtual Dosificacion IdDosificacionNavigation { get; set; }
        public virtual Pedido IdPedidoNavigation { get; set; }
        public virtual ICollection<DetalleFactura> DetalleFactura { get; set; }
    }
}
