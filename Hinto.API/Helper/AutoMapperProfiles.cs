using AutoMapper;
using Hinto.API.VM;
using Hinto.Domain.VO;
using System.Collections.Generic;

namespace Hinto.API.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<List<MidiaVO>, List<AIRecommendationMidia>>();
            CreateMap<List<ProdutorVO>, List<AIRecommendationProdutor>>();
            CreateMap<List<GeneroVO>, List<AIRecommendationGenero>>();
        }
    }
}
