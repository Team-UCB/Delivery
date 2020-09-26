using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Transportador
    {
        public Transportador()
        {
            Pedido = new HashSet<Pedido>();
        }

        public long Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Celular { get; set; }
        public string DescripcionVehiculo { get; set; }
        public string TipoVehiculo { get; set; }
        public string Estado { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
