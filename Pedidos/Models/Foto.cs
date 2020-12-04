using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Data
{
    public partial class Foto
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public string PathImg { get; set; }
        public long IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
    }
}
