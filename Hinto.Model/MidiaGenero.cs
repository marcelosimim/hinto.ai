using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class MidiaGenero
    {
        public long MidiaId { get; set; }
        public long GenerosId { get; set; }

        public Genero Generos { get; set; }
        public Midium Midia { get; set; }
    }
}
