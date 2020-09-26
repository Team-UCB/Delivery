using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Pagina
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public bool Publicado { get; set; }
        public string Tipo { get; set; }
    }
}
