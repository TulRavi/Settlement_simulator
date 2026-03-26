using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class HireWorkerAction : IUserAction
    {   private readonly HireWorkerContext HireWorkerContext;
        public WorkerEmploymentService workerEmploymentService;

        public HireWorkerAction(HireWorkerContext hireWorkerContext)
        {
            this.HireWorkerContext = hireWorkerContext;
            
        }
        public void Execute(DataWorld world)
        {
            //Worker worker = world.WorkersList.ElementAt(HireWorkerContext.WorkerIndex);
            Building building = HireWorkerContext.Building;
            Worker worker = HireWorkerContext.Worker;
            worker.AssignWithWorkPlace(building);
            building.AssignWorker(worker);//отсальное переносим в сервис
            //Building building = world.BuildingList.ElementAt(HireWorkerContext.BuildingIndex);//todo - сделано Index was out of range при попытке
            //перензначить работника


            //building.RemoveWorker();
            //workerEmploymentService.ChangeWorkingHours(world.WorkersList.ElementAt(HireWorkerContext.WorkerIndex), HireWorkerContext.StartWorkingTime, HireWorkerContext.EndWorkingTime);
            //workerEmploymentService.AssignWorker(world, worker, building);//todo - done Object reference not set to an instance of an object."
            //world.WorkerEmploymentService.ChangeWorkingHours(worker, new TimeSpan(00, 09, 00), new TimeSpan(00, 17, 00));
            //при попытке назначить работника
            //if (building.HasEmployee)
            //{
            //    Program.TempFireWorkerDirectly(world, building.AssignedWorker.WorkPlace, building.AssignedWorker);
            //    //building.AssignedWorker.UnassignWithWorkPlace();
            //}

            //worker.AssignWithWorkPlace(building);
            //building.AssignWorker(worker);
        }
    }
}
