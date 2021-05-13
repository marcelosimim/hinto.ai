using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class MidiaProdutore
    {
        public long MidiaId { get; set; }
        public long ProdutoresId { get; set; }

        public  Produtore Produtores { get; set; }
        public  Midium Midia { get; set; }
    }
}
