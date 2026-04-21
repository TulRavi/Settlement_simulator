using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class HireWorkerContext
    {
        //public WorkerEmploymentService workerEmploymentService;
        
        public Worker Worker { get; }
        public Building Building { get; }

        public HireWorkerContext(Worker worker,Building building)
        {
            Worker = worker;
            Building = building;

        }
        //public int WorkerIndex { get; }
        //public int BuildingIndex { get; }

        //public TimeSpan StartWorkingTime { get; set; }
        //public TimeSpan EndWorkingTime { get; set; }

        //public HireWorkerContext(int workerIndex, int buildingIndex, TimeSpan startWorkingTime, TimeSpan endWorkingTime)
        //{
        //    WorkerIndex = workerIndex;
        //    BuildingIndex = buildingIndex;
        //    StartWorkingTime = startWorkingTime;
        //    EndWorkingTime = endWorkingTime;
        //}
    }
}
