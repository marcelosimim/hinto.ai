using Hinto.Common.Enum;
using System;
using System.Collections.Generic;

namespace Hinto.Domain.VO
{
    public class MidiaVO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string ImagemURL { get; set; }
        public string Sinopse { get; set; }
        public TipoMidia Tipo { get; set; }
        public DateTime DataLancamento { get; set; }
        public bool Afinidade { get; set; }
        public List<ArtistaVO> Artistas { get; set; }
        public List<GeneroVO> Generos { get; set; }
    }
}
