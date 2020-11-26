using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Direccion
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }

        public string Referencia { get; set; }
        public bool Predeterminada { get; set; }
        public long IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
    }
}
