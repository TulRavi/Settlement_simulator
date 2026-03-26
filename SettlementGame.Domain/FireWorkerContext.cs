using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{   
    public class FireWorkerContext
    {
        public WorkerEmploymentService workerEmploymentService;
        public Worker Worker { get; }

        public FireWorkerContext(Worker worker)
        {
            Worker = worker;
        }
        //public int WorkerId { get; }
        //public int BuildingId { get; }

        //public FireWorkerContext(int workerId)
        //{
        //    WorkerId = workerId;
        //    BuildingId = buildingId;
        //}
    }
}
