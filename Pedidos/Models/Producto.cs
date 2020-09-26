using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Producto
    {   
        public Producto()
        {
            DetallePedido = new HashSet<DetallePedido>();
            Fotos = new HashSet<Fotos>();
            Oferta = new HashSet<Oferta>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioMayor { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Especificaciones { get; set; }
        public long IdCategoria { get; set; }
        public long IdVendedor { get; set; }

        public virtual CategoriaProducto IdCategoriaNavigation { get; set; }
        public virtual Vendedor IdVendedorNavigation { get; set; }
        public virtual ICollection<DetallePedido> DetallePedido { get; set; }
        public virtual ICollection<Fotos> Fotos { get; set; }
        public virtual ICollection<Oferta> Oferta { get; set; }
    }
}
