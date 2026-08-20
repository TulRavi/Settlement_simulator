using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public sealed class CreateBuildingAction : IUserAction // sealed prevents inheritance - remember this modifier
    {
        private readonly CreateBuildingContext CreateBuildingContext;

        public CreateBuildingAction(CreateBuildingContext CreateBuildingContext)
        {
            this.CreateBuildingContext = CreateBuildingContext;
        }

        public void Execute(DataWorld world)
        {
            Building building = new Building(CreateBuildingContext.BuildingType);
            building.X = CreateBuildingContext.X;
            building.Y = CreateBuildingContext.Y;

            CreateBuildingContext.CreatingBuilding = building;
        }
    }
}