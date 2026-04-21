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
            Building building = DestroyBuildingContext._building;
            var entity = BuildingMapper.ToEntity(building);
            DestroyBuildingContext._dbContext.BuildedBuildings.Remove(entity);
            DestroyBuildingContext._dbContext.SaveChanges();

            

            
        }
    }
}
