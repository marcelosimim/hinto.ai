using Hinto.Common.Enum;
using System;
using System.Collections.Generic;

namespace Hinto.API.VM
{
    public class AIRecommendationMidia
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string ImagemURL { get; set; }
        public string Sinopse { get; set; }
        public TipoMidia Tipo { get; set; }
        public DateTime? DataLancamento { get; set; }
        public bool? Afinidade { get; set; }
        public List<AIRecommendationProdutor> Produtores { get; set; }
        public List<AIRecommendationGenero> Generos { get; set; }


    }
}
