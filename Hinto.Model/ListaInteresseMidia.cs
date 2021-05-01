using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class ListaInteresseMidia
    {
        public long ListaInteresseId { get; set; }
        public long MidiasId { get; set; }

        public virtual ListaInteresse ListaInteresse { get; set; }
        public virtual Midium Midias { get; set; }
    }
}
