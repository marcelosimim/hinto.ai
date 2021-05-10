using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class MidiaProdutore
    {
        public long MidiaId { get; set; }
        public long ProdutoresId { get; set; }

        public virtual Midium Midia { get; set; }
        public virtual Produtore Produtores { get; set; }
    }
}
