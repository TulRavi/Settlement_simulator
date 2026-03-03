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
        public GameController(DataWorld world)
        {
            this.world = world;
        }
        
        [HttpGet]
        public ActionResult GetTime()
        {
            //string gameAndWorkingTime = $"\"gameTime\":{world.GameTime}, \n \"workingHours\":{world.startWorkingDay}-{world.endWorkingDay}";
            var response = new
            {
                gameTime = world.GameTime,
                workingHours = $"{world.startWorkingDay}-{world.endWorkingDay}"
            };
            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateWorld([FromBody]int numberOfWorkers)
        {
            WorldService.CreateWorld(world,numberOfWorkers);
            return Ok(world);
        }

        //[HttpPost]
        //public IActionResult CreateWorkerNeeds()
        //{
        //    List<Need> workerNeeds = new List<Need>();
        //    workerNeeds.Add(new HungerNeed());
        //    workerNeeds.Add(new ThirstNeed());
        //    world.BuildingList.Add(new Building(buildingType));
        //    return Ok("Building was created");
        //}

    }
    

}
