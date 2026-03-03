using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;

namespace SettlementGame.Web.Controllers
{
    [ApiController]

    [Route("api/buildings")]
    public class BuildingsController : Controller
    {
        private readonly DataWorld world;
        public BuildingsController(DataWorld world)
        {
            this.world = world;
        }
        [HttpGet]

        // GET: BuildingController
        public ActionResult GetBuildings()
        {
            return Ok(world.BuildingList);
        }
        [HttpPost]
        public IActionResult CreateBuilding([FromBody]BuildingType buildingType)
        {
            world.BuildingList.Add(new Building(buildingType));
            return Ok("Building was created");
        }

        // GET: BuildingController/Details/5
        
    }
}
