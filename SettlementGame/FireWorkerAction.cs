using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class FireWorkerAction:IUserAction
    {
        private readonly FireWorkerContext FireWorkerContext;
        
        public FireWorkerAction(FireWorkerContext FireWorkerContext)
        {
            this.FireWorkerContext = FireWorkerContext;
            
        }
        public void Execute(DataWorld world)
        {
            Worker worker = world.WorkersList.ElementAt(FireWorkerContext.WorkerIndex);
            //Building building = world.BuildingList.ElementAt(FireWorkerContext.BuildingIndex);
            
            world.WorkerEmploymentService.FireWorker(world, worker);
            //if (building.HasEmployee)
            //{
            //    building.AssignedWorker.UnassignWithWorkPlace();
            //    building.RemoveWorker();
            //}

        }
        
    }
}
