using Hinto.Common.Enum;
using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class Midium
    {
        public Midium()
        {
            this.Generos = new HashSet<Genero>();
            this.Produtores = new HashSet<Produtore>();
        }

        public long Id { get; set; }
        public bool? Afinidade { get; set; }
        public DateTime? DataLancamento { get; set; }
        public string ImagemUrl { get; set; }
        public string Sinopse { get; set; }
        public TipoMidia Tipo { get; set; }
        public string Titulo { get; set; }
        public virtual ICollection<Genero> Generos { get; set; }
        public virtual ICollection<Produtore> Produtores { get; set; }
    }
}
