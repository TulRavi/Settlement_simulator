using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class DestroyBuildingAction:IUserAction
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
            Building building = world.BuildingList.Find(x => x.BuildingId == DestroyBuildingContext.Id);
            if (building.HasEmployee==true)
            {
                DestroyBuildingContext.workerEmploymentService.FireWorker(building.AssignedWorker.Id);
                //DestroyBuildingContext.Building.
                //Program.TempFireWorkerDirectly(world, DestroyBuildingContext.Building, DestroyBuildingContext.Building.AssignedWorker);
            }
            world.BuildingList.Remove(building);
            //DestroyBuildingContext.Building.HasEmployee = false; //todo: вызвать FireworkerAction
            //CreateBuildingAction.CreateBuilding(world,buildingType);
        }
    }
}
