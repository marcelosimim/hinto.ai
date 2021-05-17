using Hinto.Domain.VO;
using System.Collections.Generic;

namespace Hinto.Domain.Contract
{
    public interface IHintoAIDomain
    {
        List<MidiaVO> Recommendations(long idUsuario);

        List<MidiaVO> Top();

    }
}
