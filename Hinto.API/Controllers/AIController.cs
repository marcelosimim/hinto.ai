using AutoMapper;
using Hinto.API.VM;
using Hinto.Domain.Contract;
using Hinto.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hinto.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIController : ControllerBase
    {

        private readonly ILogger<AIController> _logger;
        private IHintoAIDomain _hintoAIDomain;
        private IMapper _mapper;

        public AIController(ILogger<AIController> logger, IHintoAIDomain hintoAIDomain, IMapper mapper)
        {
            _logger = logger;
            _hintoAIDomain = hintoAIDomain;
            _mapper = mapper;
        }

        [Route("recommendation/{id}")]
        [HttpGet]
        public AIRecommendationViewModel Recommendations(long id) {
            return _mapper.Map<AIRecommendationViewModel>(_hintoAIDomain.Recommendations(id));
        }
    }
}
