using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;

namespace SettlementGame.Web.Controllers
{
    [ApiController]

    [Route("api/world")]

    
    public class GameController : ControllerBase
    {
        private readonly DataWorld world;
        private readonly WorldService worldService;
        public GameController(DataWorld world, WorldService worldService)
        {
            this.world = world;
            this.worldService = worldService;
        }
        //это пока уберем за ненадобностью
        //[HttpGet]
        //public ActionResult GetTime()
        //{
        //    //string gameAndWorkingTime = $"\"gameTime\":{world.GameTime}, \n \"workingHours\":{world.startWorkingDay}-{world.endWorkingDay}";
        //    var response = new
        //    {
        //        gameTime = world.GameTime,
        //        workingHours = $"{world.startWorkingDay}-{world.endWorkingDay}"
        //    };
        //    return Ok(response);
        //}

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateWorld([FromBody]int numberOfWorkers)
        {
            worldService.CreateWorld(numberOfWorkers);
            return Ok(world);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Tick()
        {
            worldService.Tick(world);
            return Ok();
        }

    }
    

}
