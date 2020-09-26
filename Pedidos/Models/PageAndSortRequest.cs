using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.Models
{
    public class PageAndSortRequest
    {
        public string Columna { get; set; }
        public string Direccion { get; set; }
        public int Pagina { get; set; }
        public int TamPagina { get; set; }
        public string Filtro { get; set; }

    }
}
