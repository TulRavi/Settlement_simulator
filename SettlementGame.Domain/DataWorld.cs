using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public  class DataWorld
    {
        //public  List<Worker> WorkersList=new  List<Worker>();
        public  List<Resource> SettlementResourceList = new List<Resource>();
        //public List<Building> BuildingList = new List<Building>();
        public List<BuildingType> PossibleBuildingList = new List<BuildingType>();
        //public WorkerEmploymentService workerEmploymentService { get; set; }
        public DateTime GameTime { get; set; }
        
        public TimeSpan startWorkingDay = new TimeSpan(9, 0, 0);
        public TimeSpan endWorkingDay = new TimeSpan(18, 0, 0);
        public int tempHoursCounter;
        private int gameState;
        public int TicksToRoad = 5;

        public int GameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        bool CrownTaskIsCompleted = false;
       
        private CrownTask currentCrownTask;

        public CrownTask CurrentCrownTask
        {
            get { return currentCrownTask; }
            set { currentCrownTask = value; }
        }

        public DataWorld()
        {
            GameTime = new DateTime(1, 1, 1);
        }
        //public int { get; set; }
        private double crownLoyaity;

        public double CrownLoyaity
        {
            get { return crownLoyaity; }
            set { crownLoyaity = Math.Clamp(value, 0, 1); }
        }

        
        bool IsGameLost => CrownLoyaity <= 0;
        bool IsGameWon => CrownLoyaity >= 1;

        

        private double peopleLoyalty;//=worker.PesonalLoyalty/WorkersList.Count()

        public double PeopleLoyalty
        {
            get { return peopleLoyalty; }
            set { peopleLoyalty = Math.Clamp(value, 0, 1); }
        }

        public double denominator { get; set; }
        public int standartSalary { get; set; }

        public class WorkerOrder
        {
            public int Amount { get; set; }

            public int TicksLeft { get; set; }
        }
        public List<WorkerOrder> WorkerOrders { get; set; } = new List<WorkerOrder>();

    }
}
