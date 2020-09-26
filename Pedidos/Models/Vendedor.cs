using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Vendedor
    {
        public Vendedor()
        {
            Pedido = new HashSet<Pedido>();
            Producto = new HashSet<Producto>();
        }

        public long Id { get; set; }
        public string PersonaContacto { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string PathLogo { get; set; }
        public long IdRubro { get; set; }

        public virtual Rubro IdRubroNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
