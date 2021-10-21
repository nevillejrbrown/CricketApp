using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CricketApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BowlController : ControllerBase
    {
        private static readonly int[] Scores = new[]
        {
            0,0,0,0,0,1,1,1,2,2,3,4,4,6
        };

        private readonly ILogger<BowlController> _logger;

        public BowlController(ILogger<BowlController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get()
        {
            var rng = new Random();
            return Scores[rng.Next(Scores.Length)];
        }
    }
}
