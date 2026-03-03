using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class HireWorkerContext
    {
        public int WorkerIndex { get; }
        public int BuildingIndex { get; }

        public TimeSpan StartWorkingTime { get; set; }
        public TimeSpan EndWorkingTime { get; set; }

        public HireWorkerContext(int workerIndex, int buildingIndex, TimeSpan startWorkingTime, TimeSpan endWorkingTime)
        {
            WorkerIndex = workerIndex;
            BuildingIndex = buildingIndex;
            StartWorkingTime = startWorkingTime;
            EndWorkingTime = endWorkingTime;
        }
    }
}
