using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    
    public class CreateBuildingContext
    {
        public BuildingType BuildingType { get; }
        public GameDbContext _dbContext { get; }
        public int X { get; }
        public int Y { get; }

        public CreateBuildingContext(BuildingType buildingType, GameDbContext dbContext, int x=0, int y=0)
        {
            BuildingType = buildingType;
            this._dbContext = dbContext;
            X = x;
            Y = y;
        }
    }
}
