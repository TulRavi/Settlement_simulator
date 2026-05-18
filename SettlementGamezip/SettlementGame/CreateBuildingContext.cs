using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class CreateBuildingContext
    {
        public BuildingType BuildingType { get; }
        public int X { get; }
        public int Y { get; }

        public CreateBuildingContext(BuildingType buildingType, int x=0, int y=0)
        {
            BuildingType = buildingType;
            X = x;
            Y = y;
        }
    }
}
