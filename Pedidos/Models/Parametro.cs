﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Pedidos.Models
{
    public partial class Parametro
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }
}