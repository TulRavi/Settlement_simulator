using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{   
    public class FireWorkerContext
    {
        //public WorkerEmploymentService workerEmploymentService;
        public Worker Worker { get; }
        public Building Building { get; }

        public FireWorkerContext(Worker worker,Building building)
        {
            Worker = worker;
            Building = building;
        }
        
    }
}
