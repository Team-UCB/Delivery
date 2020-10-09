using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallePedidos = new HashSet<DetallePedido>();
            Fotos = new HashSet<Foto>();
            Oferta = new HashSet<Ofertum>();
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
        public virtual ICollection<DetallePedido> DetallePedidos { get; set; }
        public virtual ICollection<Foto> Fotos { get; set; }
        public virtual ICollection<Ofertum> Oferta { get; set; }
    }
}
