using Hinto.Domain.VO;
using System.Collections.Generic;

namespace Hinto.Domain.Contract
{
    public interface IHintoAIDomain
    {
        AIRecommendationVO Recommendations(long idUsuario);
    }
}
