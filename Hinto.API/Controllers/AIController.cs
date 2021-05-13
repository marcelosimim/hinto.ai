using AutoMapper;
using Hinto.API.VM;
using Hinto.Domain.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
        public object Recommendations(long id) {
            return _hintoAIDomain.Recommendations(id); 
        }

        [Route("recommendation/{id}")]
        [HttpGet]
        public IActionResult Ping() {
            return Ok();
        }
    }
}
