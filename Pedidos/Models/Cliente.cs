using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedido = new HashSet<Pedido>();
        }

        public long Id { get; set; }
        public string NombresApellidos { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Referencia { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
