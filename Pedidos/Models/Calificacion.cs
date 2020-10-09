using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Calificacion
    {
        public long Id { get; set; }
        public long IdOrigen { get; set; }
        public long IdDestino { get; set; }
        public int Puntaje { get; set; }
        public string Observaciones { get; set; }
        public string Tipo { get; set; }
        public long IdPedido { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}
