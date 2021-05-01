using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class Midium
    {
        public long Id { get; set; }
        public bool? Afinidade { get; set; }
        public DateTime? DataLancamento { get; set; }
        public string ImagemUrl { get; set; }
        public string Sinopse { get; set; }
        public int Tipo { get; set; }
        public string Titulo { get; set; }
    }
}
