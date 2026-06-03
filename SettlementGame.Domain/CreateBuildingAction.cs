using Microsoft.EntityFrameworkCore;
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
            //this._dbContext= dbContext;
        }
        public void Execute(DataWorld world)
        {
            Building building = new Building(CreateBuildingContext.BuildingType);
            building.X = CreateBuildingContext.X;
            building.Y = CreateBuildingContext.Y;
            var entity = BuildingMapper.ToEntity(building);
            CreateBuildingContext.CreatingBuilding = building;
            //CreateBuildingContext._dbContext.BuildedBuildings.Add(entity);
            //CreateBuildingContext._dbContext.SaveChanges();
            //Building building = new Building(CreateBuildingContext.BuildingType);
            //убираем счетчик, ибо DB сама считает айди
            //building.Id = world.NextId;
            //world.NextId++;
            //CreateBuildingContext._dbContext.BuildedBuildings.Add(building);
            //CreateBuildingContext._dbContext.SaveChanges();
            //CreateBuildingAction.CreateBuilding(world,buildingType);
        }

        //public void CreateBuilding(DataWorld world, BuildingType buildingType)
        //{
        //    Building building = new Building(buildingType);
        //    world.BuildingList.Add(building);
        //}
    }
}
