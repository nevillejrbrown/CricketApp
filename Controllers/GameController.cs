using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace CricketApp.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {

        private static int shotSleep = 0;

        private static readonly int[] Scores = new[]
        {
            0,0,0,0,0,1,1,1,2,2,3,4,4,6
        };

        private static readonly String[] Shots = new[]
        {
            "Forward defence", "Lovely cover drive", "Ramp", "Cut", "Pull", "Late cut", "Hoik"
        };

        private static readonly String[] Balls = new[]
        {
            "Length ball", "Half volley", "Bouncer!", "Yorker", "Short ball", "Cutter", "Wide"
        };

        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("bowl")]
        public IActionResult Bowl()
        {
            var rng = new Random();

            // simulate an error 15% of the time
            if (rng.Next(100) <= 15)
            {
                return NotFound();
            }

            return Ok(Balls[rng.Next(Balls.Length)]);
        }


        [HttpGet]
        [Route("shot")]
        public IActionResult Shot()
        {
            Thread.Sleep(shotSleep);
            shotSleep += 5;
            var rng = new Random();
            return Ok(new Shot() { 
                shot = Shots[rng.Next(Shots.Length)],
                score = Scores[rng.Next(Scores.Length)] } );
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            int shotSleepWas = shotSleep;
            shotSleep = 0;
            return Ok(shotSleepWas);
        }

    }

    class Shot
    {
        public String shot { get; set; }

        public int score { get; set; }
    }
}
