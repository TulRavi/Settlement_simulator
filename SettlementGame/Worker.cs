using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Worker
    {
        public List<Need> workerNeeds;
         

        public Worker(List<Need> wokerNeeds)
        {
            this.workerNeeds = wokerNeeds;
        }
              
        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value;
                //if(IsAlive = false) { workerIsDead(как передать аргумент в свойство); }
            }
        }


        public bool IsEmployed => WorkPlace != null;
        private Building workPlace;

        public Building WorkPlace { get; private set; }

        internal void AssignWithWorkPlace(Building building)
        {
            WorkPlace = building;
            //building.HasEmployee = true;
            
        }

        internal void UnassignWithWorkPlace()
        {
            WorkPlace = null;
        }


        public void RecalculateState()
        {
            foreach (Need need in workerNeeds)                // Проверяем каждую потребность
            {
                if (need.IsCritical && need.AmountIsMoreThanOne())
                {
                    IsAlive = false;                    // Интерпретация состояния
                    return;
                }
            }
        }
        //public static void CheckIfWorkerIsDead()
        //{
        //    //foreach (var worker in Data.WorkersList)
        //    for(int w=0;w< Data.WorkersList.Count();w++)
        //    {
        //        foreach (var need in Data.WorkersList.ElementAt(w).workerNeeds)
        //        {
        //            if (need.NeedAmount >= 1)
        //            {
        //                Data.WorkersList.ElementAt(w).isAlive = false;
        //                //Data.WorkersList.Remove(worker);
        //            }
        //        }
        //    }
        //}

    }
}
