using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class HireWorkerContext
    {
                
        public Worker Worker { get; }
        public Building Building { get; }

        public HireWorkerContext(Worker worker,Building building)
        {
            Worker = worker;
            Building = building;

        }
        
        
    }
}
