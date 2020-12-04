using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Data
{
    public partial class Chat
    {
        public long Id { get; set; }
        public long IdOrigen { get; set; }
        public long IdDestino { get; set; }
        public DateTime FechaHora { get; set; }
        public string Text { get; set; }
        public string Estado { get; set; }
    }
}
