using Hinto.Common.Enum;

namespace Hinto.Domain.VO
{
    public class MidiaSimilarVO
    {
        public long MidiaId { get; set; }
        public double Similarity { get; set; }

        public RecomendationType RecomendationType { get; set; }
    }
}
