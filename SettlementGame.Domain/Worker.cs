using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class Worker
    {
        public int Id { get; set; }
        public int PersonalMoney { get; set; }

        private int workPlaceId;

        public int? WorkPlaceId
        {
            get { return workPlaceId; }
            set { workPlaceId = (int)value; }
        }

        public int? CurrentSalary;

        private string InternalId { get; set; }
        public List<Need> workerNeeds;

        [NotMapped] //SQLite will completely ignore this property
        public string? info => $"{Id} workPlceId {WorkPlaceId}";

        private double personalLoyalty = 0.5;

        public double PersonalLoyalty
        {
            get { return personalLoyalty; }
            set { personalLoyalty = Math.Clamp(value, 0, 1); }
        }

        public void ChangePersonalLoyalty(double value)
        {
            PersonalLoyalty = PersonalLoyalty + value;
        }


        public static List<Need> CreateDefaultNeeds()
        {
            List<Need> workerNeeds = new List<Need>();
            workerNeeds.Add(new NeedHunger());
            workerNeeds.Add(new NeedThirst());
            workerNeeds.Add(new NeedAlcohol());
            workerNeeds.Add(new NeedSalary());
            return workerNeeds;
        }


        public Worker(List<Need> wokerNeeds)
        {
            this.workerNeeds = wokerNeeds;
        }

        public Worker()
        {
            workerNeeds = new List<Need>();
        }

        private bool isAlive;

        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
            }
        }

        public bool IsEmployed => WorkPlaceId != null && WorkPlaceId != -1;

        public Building? WorkPlace { get; set; }

        internal void AssignWithWorkPlace(Building building)
        {
            WorkPlace = building;
            WorkPlaceId = (int)building.Id;
        }

        internal void UnassignWithWorkPlace()
        {
            WorkPlace = null;
            WorkPlaceId = -1;
        }

        //Currently not used:
        private TimeSpan startWorkingTime;

        public TimeSpan StartWorkingTime
        {
            get { return startWorkingTime; }
            set
            {
                startWorkingTime = value;
            }
        }

        private TimeSpan endWorkingTime;

        public TimeSpan EndWorkingTime
        {
            get { return endWorkingTime; }
            set
            {
                endWorkingTime = value;
                startFreeTime = endWorkingTime.Add(TimeSpan.FromMinutes(1));
                endSleepTime = StartSleepTime.Add(TimeSpan.FromHours(8));
                startSleepTime = startWorkingTime.Subtract(TimeSpan.FromHours(8));
                endFreeTime = StartSleepTime.Subtract(TimeSpan.FromMinutes(1));
            }
        }

        protected TimeSpan startFreeTime;

        protected TimeSpan StartFreeTime
        {
            get { return startFreeTime; }
            set
            {
                startFreeTime = value;
            }
        }

        protected TimeSpan endFreeTime;

        protected TimeSpan EndFreeTime
        {
            get { return endFreeTime; }
            set
            {
                endFreeTime = value;
            }
        }

        protected TimeSpan startSleepTime;

        protected TimeSpan StartSleepTime
        {
            get { return startSleepTime; }
            set
            {
                startSleepTime = value;
            }
        }

        protected TimeSpan endSleepTime;

        protected TimeSpan EndSleepTime
        {
            get { return endSleepTime; }
            set
            {
                endSleepTime = value;
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


        public void RecalculateNeedsState(Worker worker, List<Resource> resourceList)
        {
            foreach (Need need in workerNeeds)
            {
                //Step by step:
                //1. The need increases.
                need.Increase();

                //2. Check if the need has increased enough for the worker to die from hunger/thirst.
                if (need.IsCritical && need.Amount >= 1)
                {
                    isAlive = false;
                    return;
                }

                //2.1. Salary check. If the worker has a workplace, try to pay the salary.
                //If the payment fails, loyalty decreases.
                if (need is NeedSalary && worker.IsEmployed)
                {
                    bool resultSalary = need.TryToSatisfy(worker, resourceList);

                    if (resultSalary == false)
                    {
                        worker.ChangePersonalLoyalty(need.LoyaltyAmount);
                    }
                    else
                    {
                        worker.ChangeMoneyAmount((int)worker.CurrentSalary);
                    }

                    continue;
                }

                //3. Check if the need should be satisfied on the current Tick and if the worker has enough money.
                if (need.ShouldTryToSatisfy() == false)
                {
                    continue;
                }
                else if (worker.HasEnoughMoney(need.Cost) == false)
                {
                    continue;
                }

                bool result = need.TryToSatisfy(worker, resourceList);

                if (result == false)
                {
                    //If a need could not be satisfied and it is critical, loyalty decreases.
                    //Then immediately move to the next need.
                    if (need.IsCritical == true)
                    {
                        worker.ChangePersonalLoyalty(need.LoyaltyAmount);
                        continue;
                    }
                }

                if (result == true)
                {
                    //Take the money.
                    worker.ChangeMoneyAmount(need.Cost);

                    //Satisfying non-critical needs increases loyalty.
                    if (need.IsCritical == false)
                    {
                        worker.ChangePersonalLoyalty(need.LoyaltyAmount);
                    }
                }
            }
        }

        public bool HasEnoughMoney(int value)
        {
            if ((PersonalMoney - value) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeMoneyAmount(int value)
        {
            PersonalMoney = PersonalMoney + value;
        }

        public void Tick(int standartSalary, List<Resource> resourceList)
        {
            RecalculateNeedsState(this, resourceList);
        }

        //Returns the first need of the requested type from the worker's needs.
        public T GetNeed<T>() where T : Need
        {
            return workerNeeds.OfType<T>().FirstOrDefault();
        }
    }
}