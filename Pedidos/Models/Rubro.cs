using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Rubro
    {
        public Rubro()
        {
            Vendedors = new HashSet<Vendedor>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public long? IdPadre { get; set; }

        public virtual ICollection<Vendedor> Vendedors { get; set; }
    }
}
