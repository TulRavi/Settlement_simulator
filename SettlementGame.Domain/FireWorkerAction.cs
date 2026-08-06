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


            //world.SettlementResourceList.Find(x => x.ResourceType == ResourceType.Moneta).Amount -= world.standartSalary*3;
            //int compensation = world.standartSalary * 3;
            //Resource resource=new Resource(ResourceType.Moneta, compensation);
            //bool isCompleted = WorldService.ChangeResourseAmount(world, resource);
            //if (isCompleted == true) { worker.ChangeMoneyAmount(compensation);
            worker.ChangeMoneyAmount(compensation);
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

