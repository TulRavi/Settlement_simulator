using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class FireWorkerContext
    {
        public int WorkerIndex { get; }
        public int BuildingIndex { get; }

        public FireWorkerContext(int workerIndex, int buildingIndex)
        {
            WorkerIndex = workerIndex;
            BuildingIndex = buildingIndex;
        }
    }
}
