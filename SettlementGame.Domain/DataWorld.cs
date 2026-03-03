using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public  class DataWorld
    {
        public  List<Worker> WorkersList=new  List<Worker>();
        public  List<ResourceOfSettlement> ResourceList = new List<ResourceOfSettlement>();
        public List<Building> BuildingList = new List<Building>();
        public List<BuildingType> PossibleBuildingList = new List<BuildingType>();
        public WorkerEmploymentService workerEmploymentService { get; set; }
        public DateTime GameTime { get; set; }
        
        public TimeSpan startWorkingDay = new TimeSpan(9, 0, 0);
        public TimeSpan endWorkingDay = new TimeSpan(18, 0, 0);
        public int tempHoursCounter;
        public int NextWorkerId { get; set; }
        public DataWorld()
        {
            GameTime = new DateTime(1, 1, 1);
        }



    }
}
