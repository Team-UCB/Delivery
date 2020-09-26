using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
        }

        public long Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
