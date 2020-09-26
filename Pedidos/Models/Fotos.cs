using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Fotos
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public string PathImg { get; set; }
        public long IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
    }
}
