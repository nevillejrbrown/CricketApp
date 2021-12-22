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

        private static readonly String[] Umpires = new[]
        {
            "Darmesena", "Bucknor", "Illingworth"
        };

        private static readonly String[] Grounds = new[]
{
            "The Oval", "The County Ground", "Lords"
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
        public IActionResult Shot(string umpire = "Unknown" )
        {
            Thread.Sleep(shotSleep);
            shotSleep += 5;
            var rng = new Random();
            int thisScore = Scores[rng.Next(Scores.Length)];
            return Ok(new Shot() { 
                shot = Shots[rng.Next(Shots.Length)],
                score = thisScore,
                message = $"Umpire {umpire} indicates that {thisScore} has been scored"
                } );
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            int shotSleepWas = shotSleep;
            shotSleep = 0;
            return Ok(shotSleepWas);
        }

        [HttpGet]
        [Route("matchDetails")]
        public IActionResult MatchDetails()
        {
            var rng = new Random();
            return Ok(new MatchDetails()
            {
                umpire = Umpires[rng.Next(Umpires.Length)],
                ground = Grounds[rng.Next(Grounds.Length)]
            });
        }

    }

    class Shot
    {
        public string shot { get; set; }

        public int score { get; set; }

        public string message { get; set; }
    }

    class MatchDetails
    {
        public String umpire { get; set; }
        public String ground { get; set; }

    }
}
