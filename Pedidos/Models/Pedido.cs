using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            Calificacion = new HashSet<Calificacion>();
            DetallePedido = new HashSet<DetallePedido>();
            Factura = new HashSet<Factura>();
        }

        public long Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaAtencion { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; }
        public string CodigoQrFactura { get; set; }
        public decimal MontoEnvio { get; set; }
        public string TipoPago { get; set; }
        public decimal MontoCliente { get; set; }
        public long IdCliente { get; set; }
        public long IdVendedor { get; set; }
        public long IdTransporte { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Transportador IdTransporteNavigation { get; set; }
        public virtual Vendedor IdVendedorNavigation { get; set; }
        public virtual ICollection<Calificacion> Calificacion { get; set; }
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }
        public virtual ICollection<Factura> Factura { get; set; }
    }
}
