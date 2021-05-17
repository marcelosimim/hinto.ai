using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class MidiaGenero
    {
        public long MidiaId { get; set; }
        public long GenerosId { get; set; }

        public virtual Genero Generos { get; set; }
        public virtual Midium Midia { get; set; }
    }
}
