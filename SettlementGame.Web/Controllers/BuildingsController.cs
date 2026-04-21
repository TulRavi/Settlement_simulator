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
        private readonly WorkerEmploymentService workerEmploymentService;
        private readonly WorldService worldService;
        
        public BuildingsController(DataWorld world, WorkerEmploymentService workerEmploymentService, WorldService worldService)
        {
            this.world = world;
            this.workerEmploymentService = workerEmploymentService;
            this.worldService = worldService;
        }
        [HttpGet("/api/buildings")]
        

        // GET: BuildingController
        public ActionResult GetBuildings()
        {
            return Ok(worldService.GetBuildingDtoList());//todo: перенести в ВорлдСервис-done
        }

        public class CreateBuildingRequest
        {
            public BuildingType BuildingType { get; set; }
        }
        
        [HttpPost("/api/buildings")]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest createBuildingRequest)
        {
            bool result=worldService.CreateBuilding(createBuildingRequest.BuildingType);
            if (result == true) { return Ok("Building was created"); } else { return BadRequest(); }
            ;
            
        }
        [HttpDelete("/api/buildings/{id}")]
        public IActionResult RemoveBuilding([FromBody] int id)
        {
            worldService.RemoveBuilding(id);
            return Ok("Building was destroyed");
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
