using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class CategoriaProducto
    {
        public CategoriaProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
