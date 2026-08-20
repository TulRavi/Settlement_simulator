using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;

namespace SettlementGame.Web.Controllers
{
    [ApiController] //Enables API-specific behavior such as automatic model validation and binding.
    [Route("api/buildings")] //Defines the base route for all endpoints in this controller.
    public class BuildingsController : ControllerBase
    {
        private readonly WorldService worldService;

        public BuildingsController(WorldService worldService)
        {
            this.worldService = worldService;
        }

        [HttpGet("GetBuildings")]
        public IActionResult GetBuildings()
        {
            //Ok() returns HTTP 200 and serializes the Dto list into the response body.
            return Ok(worldService.GetBuildingDtoList());
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
        public IActionResult CreateBuilding(
            [FromBody] CreateBuildingRequest CreateBuildingRequest)
        {
            //[FromBody] tells ASP.NET Core to deserialize the request body into this object.
            bool result = worldService.CreateBuilding(
                CreateBuildingRequest.BuildingType);

            if (result)
            {
                return Ok("Building was Created");
            }

            //BadRequest() returns HTTP 400 when the requested operation cannot be performed.
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveBuilding(int id)
        {
            bool result = worldService.RemoveBuilding(id);

            if (result==true)
            {
                return Ok("Building was destroyed");
            }

            return BadRequest(
                "can not destroy - may be there is not enough money for worker's compensation - destroying is illegal");
        }

        [HttpGet("/api/buildings/availible")]
        public IActionResult GetAvailibleBuildings()
        {
            //This endpoint currently duplicates GetPossibleBuildings().
            //The route is kept to avoid breaking an existing client request.
            return Ok(worldService.GetPossibleBuildings());
        }
    }
}