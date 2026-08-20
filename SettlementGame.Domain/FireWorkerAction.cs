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
        public void Execute(DataWorld _world)
        {
            Worker worker = FireWorkerContext.Worker;
            
            Building building = FireWorkerContext.Building;

            int compensation= _world.standartSalary * 3;

            worker.ChangeMoneyAmount(compensation);
            worker.UnassignWithWorkPlace();
            building.RemoveWorker();
            
            
        }
        
    }
        
    }

