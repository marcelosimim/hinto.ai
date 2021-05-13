using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class Genero
    {
        public Genero()
        {
            this.Midias = new HashSet<Midium>();
        }
        public long Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Midium> Midias { get; set; }

    }
}
