using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Models
{
    public partial class Usuario
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string Estado { get; set; }
        public long IdRol { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
    }
}
