using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Data
{
    public partial class Pagina
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public bool Publicado { get; set; }
        public long Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public long? IdUsuario { get; set; }
    }
}
