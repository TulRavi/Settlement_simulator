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
        private readonly DataWorld world;
        private readonly WorkerEmploymentService workerEmploymentService;
        private readonly WorldService worldService;
        public WorkersController(DataWorld world,WorkerEmploymentService workerService, WorldService worldService)
        //DI-контейнер:видит, что нужен DataWorld,создаёт его(Singleton),
        //передаёт в контроллер - вместо new DataWorld().
        {
            this.world = world;
            this.workerEmploymentService = workerService;
            this.worldService = worldService;
        }

        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(worldService.GetWorkerDtoList());//возвращает JSON
        }

        
        public class CreateWorkerRequest
        {
            public int Number { get; set; }
        }
        [HttpPost]
        public IActionResult CreateWorker([FromBody] CreateWorkerRequest request)
        {
            worldService.CreateWorkersByService(request.Number);
            return Ok($"New Worker(s) {request.Number} were created");
        }
        [HttpGet("{id}")]
        public IActionResult GetWorkerByID(int id)
        {
            bool isFound = workerEmploymentService.GetWorkerById(id);
            if (isFound == true) 
            {
                List<WorkerDto> workerDtoList= worldService.GetWorkerDtoList();
                return Ok(workerDtoList.Find(x=>x.Id==id)); } else { return NotFound(); 
            }
        }

        [HttpDelete("/api/workers/{id}")]
        public IActionResult DeleteWorkerbyID(int id)
        {
            bool isDeleted=workerEmploymentService.DeleteWorker(id);
            if (isDeleted == true) {
                return Ok($"Worker ID {id} was removed");
            }
            else { return NotFound(); }
            
        }

        [HttpDelete]
        public IActionResult ClearWorkersListByUser()
        {
            workerEmploymentService.ClearWorkersList();  
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
            public int BuildingId { get; set; }
            
        }

        [HttpPost("{id}/hire")]
        public IActionResult HireWorker(HireWorkerRequest hireWorkerRequest)
        {
            bool result = workerEmploymentService.HireWorker(hireWorkerRequest.WorkerId, hireWorkerRequest.BuildingId);
            if (result == true)
            {
                return Ok($"Worker ID {hireWorkerRequest.WorkerId} workPlace was changed");
            }
            else { return NotFound(); }
        }

        
        [HttpDelete("{id}/fire")]
        public IActionResult FireWorker(int id)
        {
            bool result = workerEmploymentService.FireWorker(id);
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
            //public bool IsEmployed { get; set; }
        }

        [HttpPut("{id}")]
        public IActionResult ChangeWorker(int id, ReplaceWorkerRequest replaceWorkerRequest)
        {
            //int temp = 0;
            bool isFound = false;
            workerEmploymentService.FindWorker(id, out isFound);
            if (isFound == true)
            {
                workerEmploymentService.ChangeWorker(id, replaceWorkerRequest.X, replaceWorkerRequest.Y, replaceWorkerRequest.IsAlive);
                //world.WorkersList.ElementAt(temp).id = replaceWorkerRequest.Id;
                //world.WorkersList.ElementAt(temp).X = replaceWorkerRequest.X;
                //world.WorkersList.ElementAt(temp).Y = replaceWorkerRequest.Y;
                //world.WorkersList.ElementAt(temp).IsAlive = replaceWorkerRequest.IsAlive;
                
                //world.WorkersList.RemoveAt(temp);
                //world.WorkersList.Insert(temp,worker);

                //PropertyInfo[] properties = typeof(Worker).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                //    foreach (var prop in properties)
                //    {
                //        if (prop.CanWrite) // Проверяем, можно ли записать в свойство
                //        {
                //            var value = prop.GetValue(worker);
                //            prop.SetValue(world.WorkersList.ElementAt(temp), value);
                //        }
                //    }
                // все объекты в списке имеют свойства как у source


                return Ok($"Worker ID {id} was replaced/changed");
            }
            else { return BadRequest(); }
        }

    }
}
