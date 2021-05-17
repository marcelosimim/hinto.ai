using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class ListaFavoritosMidia
    {
        public long ListaFavoritosId { get; set; }
        public long MidiasId { get; set; }

        public virtual Midium Midias { get; set; }
    }
}
