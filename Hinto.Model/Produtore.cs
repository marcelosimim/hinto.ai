using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class Produtore
    {
        public Produtore()
        {
            this.Midias = new HashSet<Midium>();
        }
        public long Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Midium> Midias { get; set; }
    }
}
