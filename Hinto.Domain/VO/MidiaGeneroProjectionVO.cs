using System.Collections.Generic;
using System.Linq;

namespace Hinto.Domain.VO
{
    public class MidiaGeneroProjectionVO
    {
        public long MidiaId { get; set; }
        public List<long> GenerosId { get; set; }
    }
}
