using System;
using System.Collections.Generic;

namespace Pedidos.Models
{
    public partial class Mensajes
    {
        public long Id { get; set; }
        public DateTime FechaHora { get; set; }
        public string Text { get; set; }
        public long IdChat { get; set; }

        public virtual Chat IdChatNavigation { get; set; }
    }
}
