using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class HireWorkerAction : IUserAction
    {   private readonly HireWorkerContext HireWorkerContext;
        
        public HireWorkerAction(HireWorkerContext HairWorkerContext)
        {
            this.HireWorkerContext = HairWorkerContext;
            
        }
        public void Execute(DataWorld world)
        {
            Worker worker = world.WorkersList.ElementAt(HireWorkerContext.WorkerIndex);
            Building building = world.BuildingList.ElementAt(HireWorkerContext.BuildingIndex);//todo - сделано Index was out of range при попытке
            //перензначить работника
            world.WorkerEmploymentService.AssignWorker(world, worker, building);
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
