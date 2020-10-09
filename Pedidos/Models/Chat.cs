using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Chat
    {
        public Chat()
        {
            Mensajes = new HashSet<Mensaje>();
        }

        public long Id { get; set; }
        public long IdOrigen { get; set; }
        public long IdDestino { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Mensaje> Mensajes { get; set; }
    }
}
