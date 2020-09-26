using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.Models
{
    public class PageAndSortResponse<T>
    {
        public IEnumerable<T> Datos { get; set; }
        public int TotalFilas { get; set; }

    }
}
