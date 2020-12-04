using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Data
{
    public partial class Cliente
    {
        public Cliente()
        {
            Direccions = new HashSet<Direccion>();
            Pedidos = new HashSet<Pedido>();
        }

        public long Id { get; set; }
        public string NombresApellidos { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Direccion> Direccions { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
