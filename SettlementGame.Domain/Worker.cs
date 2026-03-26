using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class Worker
    {
        public string Position { get; set; }
        public int Id { get; set; }
        private string InternalId { get; set; }
        public List<Need> workerNeeds;
        //public TimeSpan startWorkingTime { get; set; }
        //public TimeSpan endWorkingTime { get; set; }

        private TimeSpan startWorkingTime;

        public TimeSpan StartWorkingTime
        {
            get { return startWorkingTime; }
            set { startWorkingTime = value;
                //CurrentTimeForm = "works";

            }
        }
        private TimeSpan endWorkingTime;
        public TimeSpan EndWorkingTime
        {
            get { return endWorkingTime; }
            set {
                endWorkingTime = value;
                //CurrentTimeForm = "works";
                startFreeTime = endWorkingTime.Add(TimeSpan.FromMinutes(1));
                endSleepTime = StartSleepTime.Add(TimeSpan.FromHours(8));
                startSleepTime = startWorkingTime.Subtract(TimeSpan.FromHours(8));
                endFreeTime = StartSleepTime.Subtract(TimeSpan.FromMinutes(1));
            }
        }

        //public TimeSpan startFreeTime { get; set; }
        //public TimeSpan endFreeTime { get; set; }
        //public TimeSpan startSleepTime { get; set; }
        //public TimeSpan endSleepTime { get; set; }

        protected TimeSpan startFreeTime;
        protected TimeSpan StartFreeTime
        {
            get { return startFreeTime; }
            set {
                startFreeTime = value;
                //startFreeTime = endWorkingTime.Add(TimeSpan.FromMinutes(1));
                //CurrentTimeForm = "rests";
            }
        }

        protected TimeSpan endFreeTime;
        protected TimeSpan EndFreeTime
        {
            get { return endFreeTime; }
            set {
                endFreeTime = value;
                //endFreeTime = StartSleepTime.Add(TimeSpan.FromMinutes(-1));

            }
        }

        protected TimeSpan startSleepTime;
        protected TimeSpan StartSleepTime
        {
            get { return startSleepTime; }
            set {
                startSleepTime = value;
                //startSleepTime = startWorkingTime.Add(TimeSpan.FromHours(-8));
                //CurrentTimeForm = "sleeps";
            }
        }

        protected TimeSpan endSleepTime;
        protected TimeSpan EndSleepTime
        {
            get { return endSleepTime; }
            set
            {
                endSleepTime = value;
                //endSleepTime = StartSleepTime.Add(TimeSpan.FromHours(8)); }
            }
        }
        protected string currentTimeForm;
        public string CurrentTimeForm
        {
            get { return currentTimeForm; }
            protected set { currentTimeForm = value; }
        }

        public int X { get; set; }
        public int Y { get; set; }

        
        private double personalLoyality;

        public double PersonalLoyality
        {
            get { return personalLoyality; }
            set { personalLoyality = Math.Clamp(value, 0, 1); }
        }
        public void ChangePersonalLoyality(double value)
        {
            personalLoyality = personalLoyality + value;
        }



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


        public void RecalculateNeedsState(DataWorld world)
        {
            foreach (Need need in workerNeeds)                // Проверяем каждую потребность
            {
                if (need.IsCritical && need.AmountIsMoreThanOne())
                {
                    IsAlive = false;                    // Интерпретация состояния
                    return;
                }
                if (need.AmountIsMoreThanOne()==false)//если работник не умер удволетврояем все нужды за деньги
                {
                    
                    if (HasEnoughMoney(need.Cost)==true)
                    {   
                        bool isConfirmed=need.ChangePerTick(world);
                        if (isConfirmed == true) { ChangeMoneyAmount(need.Cost); }
                        ChangePersonalLoyality(need.LoyalityAmount);
                        
                    }
                }
            }
        }
        public void RecalculateLoyality()
        {
            //если базовые потребности удоволтеворены, то удоплетворение доп.потребностей дает рост, если не удовлотетоврены базовые, минус.
        }

        private int PersonalMoney { get; set; }
        
        public bool HasEnoughMoney(int value)
        {
            if ((PersonalMoney - value) >= 0)
            {
                //PersonalMoney = PersonalMoney - value;
                return true;
            }
            else return false;   
        }
        public void ChangeMoneyAmount(int value)
        {
            PersonalMoney = PersonalMoney - value;
        }

        public void Tick(DataWorld world)
        {
            RecalculateNeedsState(world);
            

        }





    }
}
