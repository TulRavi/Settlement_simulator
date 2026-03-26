using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public sealed class CreateBuildingAction: IUserAction//модификатор sealed запрещает наследование - взять на вооружение
    {
        private readonly CreateBuildingContext CreateBuildingContext;
        public CreateBuildingAction(CreateBuildingContext CreateBuildingContext)
        {
            this.CreateBuildingContext = CreateBuildingContext;
        }
        public void Execute(DataWorld world)
        {
            Building building = new Building(CreateBuildingContext.BuildingType);
            building.BuildingId = world.NextBuildingId;
            world.NextBuildingId++;
            world.BuildingList.Add(building);

            //CreateBuildingAction.CreateBuilding(world,buildingType);
        }

        //public void CreateBuilding(DataWorld world, BuildingType buildingType)
        //{
        //    Building building = new Building(buildingType);
        //    world.BuildingList.Add(building);
        //}
    }
}
