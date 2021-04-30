using AutoMapper;
using Hinto.API.VM;
using Hinto.Domain.VO;

namespace Hinto.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AIRecommendationVO, AIRecommendationViewModel>();
        }
    }
}
