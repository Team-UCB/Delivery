using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Rubro
    {
        public Rubro()
        {
            Vendedor = new HashSet<Vendedor>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Vendedor> Vendedor { get; set; }
    }
}
