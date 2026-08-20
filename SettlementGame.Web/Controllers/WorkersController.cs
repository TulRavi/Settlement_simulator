using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;

namespace SettlementGame.Web.Controllers
{
    [ApiController] //Shows that this controller is an API controller and enables API-specific behavior.
    [Route("api/workers")] //Defines the base URL for worker-related endpoints.
    public class WorkersController : ControllerBase
    {
        private readonly DataWorld _world;
        private readonly WorkerEmploymentService _workerEmploymentService;
        private readonly WorldService _worldService;

        public WorkersController(
            DataWorld world,
            WorkerEmploymentService workerService,
            WorldService worldService)
        {
            //The DI container sees that the controller needs DataWorld,
            //Creates or retrieves the registered instance and passes it to the controller
            //instead of creating it manually with new DataWorld().
            this._world = world;
            this._workerEmploymentService = workerService;
            this._worldService = worldService;
        }

        [HttpGet("GetWorkersDto")]
        public IActionResult GetWorkersDto()
        {
            return Ok(_worldService.GetWorkerDtoList()); //ASP.NET Core serializes the List into JSON.
        }

        [HttpGet("GetTicksTillNewWorkers")]
        public IActionResult GetTicksForNewWorkers()
        {
            return Ok(_workerEmploymentService.GetTicksTillNewWorkers()); //ASP.NET Core serializes the List into JSON.
        }

        public class CreateWorkerRequest
        {
            public int Number { get; set; }
        }

        [HttpPost]
        public IActionResult CreateWorker([FromBody] CreateWorkerRequest request)
        {
            _worldService.CreateWorkersByService(request.Number);

            return Ok($"New Worker(s) {request.Number} were Created");
        }

        [HttpPost("CreateOrderForNewWorkers")]
        public IActionResult CreateOrderForNewWorkers(
            [FromBody] CreateWorkerRequest request)
        {
            bool isAsked = _workerEmploymentService.CreateOrderForNewWorkers(
                request.Number,
                _world);

            if (isAsked == true)
            {
                return Ok($"New Worker(s) {request.Number} were req");
            }
            else
            {
                return BadRequest("not enough money");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetWorkerById(int id)
        {
            var worker = _workerEmploymentService.GetWorkerById(id);

            if (worker == null)
                return NotFound();

            return Ok(worker);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkerByID(int id)
        {
            bool isDeleted = _workerEmploymentService.DeleteWorker(id);

            if (isDeleted == true)
            {
                return Ok($"Worker ID {id} was removed");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public IActionResult ClearWorkersListByUser()
        {
            _workerEmploymentService.ClearWorkersList();

            return Ok("Worker list was cleared");
        }

        public class HireWorkerRequest
        {
            public int WorkerId { get; set; }
            public int BuildingId { get; set; }
        }

        [HttpPut("hire")]
        public IActionResult HireWorker(
            [FromBody] HireWorkerRequest hireWorkerRequest)
        {
            bool result = _workerEmploymentService.HireWorker(
                hireWorkerRequest.WorkerId,
                hireWorkerRequest.BuildingId);

            if (result == true)
            {
                return Ok(
                    $"Worker ID {hireWorkerRequest.WorkerId} workPlace was changed");
            }
            else
            {
                return BadRequest("impossible to execute-check money amount");
            }
        }

        [HttpPut("{id}/fire")]
        public IActionResult FireWorker(int id)
        {
            bool result = _workerEmploymentService.FireWorker(id);

            if (result == true)
            {
                return Ok($"Worker ID {id} was fired");
            }
            else
            {
                return NotFound();
            }
        }

        public class ReplaceWorkerRequest
        {
            //public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsAlive { get; set; }
            public bool IsEmployed { get; set; }
            public double PersonalLoyality { get; set; }
            //public bool IsEmployed { get; set; }
        }

        [HttpPut("{id}")]
        public IActionResult ChangeWorker(
            int id,
            [FromBody] ReplaceWorkerRequest replaceWorkerRequest)
        {
            //FindWorker returns true if a worker with this ID exists.
            bool isFound = _workerEmploymentService.FindWorker(id);

            if (isFound == true)
            {
                _workerEmploymentService.ChangeWorker(
                    id,
                    replaceWorkerRequest.X,
                    replaceWorkerRequest.Y,
                    replaceWorkerRequest.IsAlive,
                    replaceWorkerRequest.IsEmployed,
                    replaceWorkerRequest.PersonalLoyality);

                return Ok($"Worker ID {id} was replaced/changed");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}