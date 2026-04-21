using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class DestroyBuildingContext
    {
        public WorkerEmploymentService workerEmploymentService { get; }
        public GameDbContext _dbContext { get; }
       public Building _building { get; }
        public int X { get; }
        public int Y { get; }

        public int Id;

        

        public DestroyBuildingContext(Building building, GameDbContext dbContext, int x = 0, int y = 0)
        {
            //Building = building;
            X = x;
            Y = y;
            //Id = id;
            this._building = building;
            this._dbContext = dbContext;
        }
    }
}
