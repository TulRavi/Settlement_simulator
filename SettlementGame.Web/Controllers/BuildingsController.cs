using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;

namespace SettlementGame.Web.Controllers
{
    [ApiController]

    [Route("api/buildings")]
    public class BuildingsController : Controller
    {
        private readonly DataWorld world;
        private readonly WorkerEmploymentService workerEmploymentService;
        private readonly WorldService worldService;
        
        public BuildingsController(DataWorld world, WorkerEmploymentService workerEmploymentService, WorldService worldService)
        {
            this.world = world;
            this.workerEmploymentService = workerEmploymentService;
            this.worldService = worldService;
        }
        [HttpGet("GetBuildings")]
        

        // GET: BuildingController
        public ActionResult GetBuildings()
        {
            return Ok(worldService.GetBuildingDtoList());//todo: перенести в ВорлдСервис-done
        }

        [HttpGet("GetPossibleBuildings")]
        public IActionResult GetPossibleBuildings()
        {
            return Ok(worldService.GetPossibleBuildings());
        }

        public class CreateBuildingRequest
        {
            public BuildingType BuildingType { get; set; }
        }
        
        [HttpPost("CreateBuilding")]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest createBuildingRequest)
        {
            bool result=worldService.CreateBuilding(createBuildingRequest.BuildingType);
            if (result == true) { return Ok("Building was created"); } else { return BadRequest(); }
            ;
            
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveBuilding(int id)
        {
            bool result = worldService.RemoveBuilding(id);
            
            if (result == true) { return Ok("Building was destroyed"); } else { return BadRequest("not enouth money for compensation to a worker - destroying is illegal"); }
        }

        // GET: BuildingController/Details/5
        [HttpGet("/api/buildings/availible")]

        // GET: BuildingController
        public ActionResult GetAvailibleBuildings()
        {
            return Ok(worldService.GetPossibleBuildings());
        }
    }
}
