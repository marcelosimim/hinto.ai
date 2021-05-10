using System.Collections.Generic;

namespace Hinto.Domain.VO
{
    public class MidiaProdutoreProjectionVO
    {
        public long MidiaId { get; set; }
        public List<long> Produtores { get; set; }
    }
}
