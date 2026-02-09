using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class DestroyBuildingContext
    {
        public Building Building { get; }
        public int X { get; }
        public int Y { get; }

        public DestroyBuildingContext(Building building, int x = 0, int y = 0)
        {
            Building = building;
            X = x;
            Y = y;
        }
    }
}
