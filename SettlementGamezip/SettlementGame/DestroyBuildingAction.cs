using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class DestroyBuildingAction:IUserAction
    {
        private readonly DestroyBuildingContext DestroyBuildingContext;
        
        public DestroyBuildingAction(DestroyBuildingContext DestroyBuildingContext)
        {
            this.DestroyBuildingContext = DestroyBuildingContext;
            
        }
        public void Execute(DataWorld world)
        {
            //DestroyBuildingContext.Building=null;
            //todo: мы убрали ссылку, но отовсюду ли? проверить
            //DestroyBuildingContext.Building.AssignedWorker.

            //DestroyBuildingContext.Building.AssignedWorker.UnassignWithWorkPlace();
            //DestroyBuildingContext.Building.RemoveWorker();
            if (DestroyBuildingContext.Building.HasEmployee==true)
            {
                world.WorkerEmploymentService.FireWorker(world, DestroyBuildingContext.Building.AssignedWorker);
                //DestroyBuildingContext.Building.
                //Program.TempFireWorkerDirectly(world, DestroyBuildingContext.Building, DestroyBuildingContext.Building.AssignedWorker);
            }
            world.BuildingList.Remove(DestroyBuildingContext.Building);
            //DestroyBuildingContext.Building.HasEmployee = false; //todo: вызвать FireworkerAction
            //CreateBuildingAction.CreateBuilding(world,buildingType);
        }
    }
}
