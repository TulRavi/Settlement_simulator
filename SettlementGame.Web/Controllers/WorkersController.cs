using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SettlementGame.Domain;
using System.Reflection;
using static SettlementGame.Domain.WorldService;

namespace SettlementGame.Web.Controllers
{
    [ApiController]//показывает принадлежность к ApiController
    [Route("api/workers")] //пишем адрес
    public class WorkersController : ControllerBase
    {
        private readonly DataWorld world;
        public WorkersController(DataWorld world) //DI-контейнер:видит, что нужен DataWorld,создаёт его(Singleton),
                                                  //передаёт в контроллер - вместо new DataWorld().
        {
            this.world = world;
        }
        [HttpGet]
        public IActionResult GetWorkers()
        {
            return Ok(WorldService.GetWorkerDtoList(world));//возвращает JSON
        }

        
        public class CreateWorkerRequest
        {
            public int Number { get; set; }
        }
        [HttpPost]
        public IActionResult CreateWorker([FromBody] CreateWorkerRequest request)
        {
            WorldService.CreateWorkersByService(request.Number, world);
            //    for (int i = 0; i < request.Number; i++)
            //    {
            //        //Random random = new Random();

            //        List<Need> workerNeeds = new List<Need>
            //{
            //    new HungerNeed(),
            //    new ThirstNeed()
            //};

            //        Worker worker = new Worker(workerNeeds);
            //        //worker.id = random.Next(0, 999);
            //        worker.Id = world.NextWorkerId;
            //        world.WorkersList.Add(worker);
            //        world.NextWorkerId++;
            //    }

            return Ok($"New Worker(s) {request.Number} were created");
        }
        [HttpGet("{id}")]
        public IActionResult GetWorkerByID(int id)
        {
            int temp=0;
            bool isFound=false;
            world.workerEmploymentService.FindWorker(world, id,out temp,out isFound);
            //int temp=0;
            //bool isFound = false;
            //for (int i = 0; i < world.WorkersList.Count; i++)
            //{
            //    if (world.WorkersList.ElementAt(i).id == id)
            //    {
            //        temp = i;
            //        isFound = true;
            //        break;
            //    }
            //}
            if (isFound == true) 
            {
                List<WorkerDto> workerDtoList= WorldService.GetWorkerDtoList(world);
                return Ok(workerDtoList.ElementAt(temp)); } else { return NotFound(); }
            //return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWorkerbyID(int id)
        {
            bool isDeleted=WorkerEmploymentService.DeleteWorker(world, id);
            if (isDeleted == true) {
                return Ok($"Worker ID {id} was removed");
            }
            else { return NotFound(); }
            //int temp = 0;
            //bool isFound = false;
            //world.workerEmploymentService.FindWorker(world, id, out temp, out isFound);
            //if (isFound == true) 
            //{
            //    world.WorkersList.RemoveAt(temp);
            //    return Ok($"Worker ID {id} was removed"); } else { return NotFound();
            //}
        }

        [HttpDelete]
        public IActionResult ClearWorkersListByUser()
        {
            WorkerEmploymentService.ClearWorkersList(world);  
         return Ok($"Worker list was cleared");
            
        }

        public class ChangeWorkerStateRequest
        {
            public bool isAlive { get; set; }
        }

        [HttpPatch("{id}")]
        public IActionResult ChangeWorkersLifeState(int id, ChangeWorkerStateRequest changeAliveState)
        {
            int temp = 0;
            bool isFound = false;
            world.workerEmploymentService.FindWorker(world, id, out temp, out isFound);
            if (isFound == true) {
                world.WorkersList.ElementAt(temp).IsAlive = changeAliveState.isAlive;
                return Ok($"Worker ID {id} isAliveState was changed to {changeAliveState.isAlive}"); } else { return NotFound(); }
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
            int temp = 0;
            bool isFound = false;
            world.workerEmploymentService.FindWorker(world, id, out temp, out isFound);
            if (isFound == true)
            {
                //world.WorkersList.ElementAt(temp).id = replaceWorkerRequest.Id;
                world.WorkersList.ElementAt(temp).X = replaceWorkerRequest.X;
                world.WorkersList.ElementAt(temp).Y = replaceWorkerRequest.Y;
                world.WorkersList.ElementAt(temp).IsAlive = replaceWorkerRequest.IsAlive;
                
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
                // Результат: все объекты в списке имеют свойства как у source


                return Ok($"Worker ID {id} was replaced/changed");
            }
            else { return BadRequest(); }
        }

    }
}
