using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;
using System.Reflection;
using static SettlementGame.Domain.WorldService;
using static System.Net.WebRequestMethods;

namespace SettlementGame.Web.Controllers
{
    [ApiController]//показывает принадлежность к ApiController
    [Route("api/workers")] //пишем адрес
    public class WorkersController : ControllerBase
    {
        private readonly DataWorld _world;
        private readonly WorkerEmploymentService _workerEmploymentService;
        private readonly WorldService _worldService;
        public WorkersController(DataWorld world,WorkerEmploymentService workerService, WorldService worldService)
        //DI-контейнер:видит, что нужен DataWorld,создаёт его(Singleton),
        //передаёт в контроллер - вместо new DataWorld().
        {
            this._world = world;
            this._workerEmploymentService = workerService;
            this._worldService = worldService;
        }

        [HttpGet("GetWorkersDTO")]
        public IActionResult GetWorkersDTO()
        {
            return Ok(_worldService.GetWorkerDtoList()); // ASP превращает List в JSON
        }

        [HttpGet("GetTicksTillNewWorkers")]
        public IActionResult GetTicksForNewWorkers()
        {
            return Ok(_workerEmploymentService.GetTicksTillNewWorkers()); // ASP превращает List в JSON
        }


        public class CreateWorkerRequest
        {
            public int Number { get; set; }
        }
        [HttpPost]
        public IActionResult CreateWorker(CreateWorkerRequest request)
        {
            _worldService.CreateWorkersByService(request.Number);
            return Ok($"New Worker(s) {request.Number} were created");
        }

        [HttpPost("CreateOrderForNewWorkers")]
        public IActionResult CreateOrderForNewWorkers([FromBody] CreateWorkerRequest request)
        {
            bool isAsked=_workerEmploymentService.CreateOrderForNewWorkers(request.Number,_world);

            if (isAsked == true) { return Ok($"New Worker(s) {request.Number} were req"); } else return BadRequest("not enough money");
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
        public IActionResult DeleteWorkerbyID(int id)
        {
            bool isDeleted=_workerEmploymentService.DeleteWorker(id);
            if (isDeleted == true) {
                return Ok($"Worker ID {id} was removed");
            }
            else { return NotFound(); }
            
        }

        [HttpDelete]
        public IActionResult ClearWorkersListByUser()
        {
            _workerEmploymentService.ClearWorkersList();  
         return Ok($"Worker list was cleared");
            
        }
        //более не нужен
        //public class ChangeWorkerStateRequest
        //{
        //    public bool isAlive { get; set; }
        //}

        //[HttpPatch("{id}")]
        //public IActionResult ChangeWorkersLifeState(int id, ChangeWorkerStateRequest changeAliveState)
        //{
        //    //int temp = 0;
        //    bool isFound = false;
        //    workerEmploymentService.FindWorker(id, out isFound);
        //    if (isFound == true) {
        //        bool isAlive = changeAliveState.isAlive;
        //        //world.WorkersList.ElementAt(temp).IsAlive = changeAliveState.isAlive;
        //        workerEmploymentService.ChangeWorkersLifeState(id, isAlive);
        //        return Ok($"Worker ID {id} isAliveState was changed to {changeAliveState.isAlive}"); } else { return NotFound(); }
        //}

        public class HireWorkerRequest
        {
            public int WorkerId { get; set; }
            public int Id { get; set; }
            
        }

        [HttpPut("hire")]
        public IActionResult HireWorker(HireWorkerRequest hireWorkerRequest)
        {
            bool result = _workerEmploymentService.HireWorker(hireWorkerRequest.WorkerId, hireWorkerRequest.Id);
            if (result == true)
            {
                return Ok($"Worker ID {hireWorkerRequest.WorkerId} workPlace was changed");
            }
            else { return BadRequest("impossible to execute-check money amount"); }
        }

        
        [HttpPut("{id}/fire")]
        public IActionResult FireWorker(int id)
        {   
            bool result = _workerEmploymentService.FireWorker(id);
            if (result == true)
            {
                return Ok($"Worker ID {id} was fired");
            }
            else { return NotFound(); }
        }


        public class ReplaceWorkerRequest
        {
            //public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsAlive { get; set; }
            public bool IsEmployed { get; set; }
            public double PersonalLoaylity { get; set; }
            //public bool IsEmployed { get; set; }
        }

        [HttpPut("{id}")]
        public IActionResult ChangeWorker(int id, ReplaceWorkerRequest replaceWorkerRequest)
        {
            //int temp = 0;
            bool isFound = false;
            _workerEmploymentService.FindWorker(id);
            if (isFound == true)
            {
                _workerEmploymentService.ChangeWorker(id, replaceWorkerRequest.X, replaceWorkerRequest.Y, replaceWorkerRequest.IsAlive,replaceWorkerRequest.IsEmployed,replaceWorkerRequest.PersonalLoaylity);
                

                return Ok($"Worker ID {id} was replaced/changed");
            }
            else { return BadRequest(); }
        }

    }
}
