using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class MidiaArtista
    {
        public long MidiaId { get; set; }
        public long ArtistasId { get; set; }

        public virtual Artistum Artistas { get; set; }
        public virtual Midium Midia { get; set; }
    }
}
