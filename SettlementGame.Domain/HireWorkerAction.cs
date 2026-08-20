using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class HireWorkerAction : IUserAction
    {   private readonly HireWorkerContext HireWorkerContext;
        

        public HireWorkerAction(HireWorkerContext hireWorkerContext)
        {
            this.HireWorkerContext = hireWorkerContext;
            
        }
        public void Execute(DataWorld world)
        {
            
            Building building = HireWorkerContext.Building;
            Worker worker = HireWorkerContext.Worker;
            worker.AssignWithWorkPlace(building);
            building.AssignWorker(worker);//other func. were removed to service
            
        }
    }
}
