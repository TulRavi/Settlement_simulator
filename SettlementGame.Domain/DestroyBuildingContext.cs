using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class DestroyBuildingContext
    {
        public WorkerEmploymentService workerEmploymentService;
        //public Building Building { get; }
        public int X { get; }
        public int Y { get; }

        public int Id;

        public DestroyBuildingContext(int id, int x = 0, int y = 0)
        {
            //Building = building;
            X = x;
            Y = y;
            Id = id;
        }
    }
}
