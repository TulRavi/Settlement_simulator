using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class FireWorkerAction:IUserAction
    {
        private readonly FireWorkerContext FireWorkerContext;
        
        public FireWorkerAction(FireWorkerContext FireWorkerContext)
        {
            this.FireWorkerContext = FireWorkerContext;
            
        }
        public void Execute(DataWorld world)
        {
            Worker worker = FireWorkerContext.Worker;

            Building building = worker.WorkPlace;

            worker.UnassignWithWorkPlace();
            building.RemoveWorker();
        }
        //if (worker.WorkPlace != null)
        //{
        //    Building building = worker.WorkPlace;
        //    worker.UnassignWithWorkPlace();
        //    building.RemoveWorker();
        //}
    }
        
    }

