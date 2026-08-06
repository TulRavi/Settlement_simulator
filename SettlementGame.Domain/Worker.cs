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
        //public string Position { get; set; }
        public int Id { get; set; }
        public int PersonalMoney { get; set; }

        private int workPlaceId;

        public int ? WorkPlaceId
        {
            get { return workPlaceId; }
            set { workPlaceId = (int)value; }
        }

        public int?CurrentSalary;

        //public int?WorkPlaceId { get; set; }
        private string InternalId { get; set; }
        public List<Need> workerNeeds;
        //public TimeSpan startWorkingTime { get; set; }
        //public TimeSpan endWorkingTime { get; set; }
        [NotMapped] // SQLite полностью проигнорирует это свойство
        public string ? info => $"{Id} workPlceId {WorkPlaceId}";

        private double personalLoyality=0.5;

        public double PersonalLoyality
        {
            get { return personalLoyality; }
            set { personalLoyality = Math.Clamp(value, 0, 1); }
        }
        public void ChangePersonalLoyality(double value)
        {
            personalLoyality = PersonalLoyality + value;
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
            set { isAlive = value;
                //if(IsAlive = false) { workerIsDead(как передать аргумент в свойство); }
            }
        }

        public bool IsEmployed => WorkPlaceId != null && WorkPlaceId != -1;
        //private Building? workPlace;

        public Building? WorkPlace { get;  set; }

        internal void AssignWithWorkPlace(Building building)
        {
            WorkPlace = building;
            WorkPlaceId = (int)building.Id;
            //building.HasEmployee = true;
            
        }

        internal void UnassignWithWorkPlace()
        {
            WorkPlace = null;
            WorkPlaceId = -1;
        }

        //пока не исп-ся:
        private TimeSpan startWorkingTime;

        public TimeSpan StartWorkingTime
        {
            get { return startWorkingTime; }
            set
            {
                startWorkingTime = value;
                //CurrentTimeForm = "works";

            }
        }
        private TimeSpan endWorkingTime;
        public TimeSpan EndWorkingTime
        {
            get { return endWorkingTime; }
            set
            {
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
            set
            {
                startFreeTime = value;
                //startFreeTime = endWorkingTime.Add(TimeSpan.FromMinutes(1));
                //CurrentTimeForm = "rests";
            }
        }

        protected TimeSpan endFreeTime;
        protected TimeSpan EndFreeTime
        {
            get { return endFreeTime; }
            set
            {
                endFreeTime = value;
                //endFreeTime = StartSleepTime.Add(TimeSpan.FromMinutes(-1));

            }
        }

        protected TimeSpan startSleepTime;
        protected TimeSpan StartSleepTime
        {
            get { return startSleepTime; }
            set
            {
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

        //конец неисп.свойств


        public void RecalculateNeedsState(Worker worker, List<Resource> resourceList) 
        {   foreach(Need need in workerNeeds)
            {   //пошагово
                //1 нужда растет
                need.Increase();
                //2 проверяем, не выросла ли нужда настолько, что рабочий умер от голода/жажды
                if (need.IsCritical && need.Amount >= 1)
                {
                    isAlive = false;
                    return;
                }

                //2.1 проверка ЗП. есть рабочее место-пробуем платить. не получается - снижается лояльность
                
                if (need is NeedSalary && worker.IsEmployed)
                {
                    bool resultSalary = need.TryToSaticfy(worker, resourceList);
                    if (resultSalary == false) { worker.ChangePersonalLoyality(need.LoyalityAmount); } else { worker.ChangeMoneyAmount((int)worker.CurrentSalary); }
                    continue;
                }
                //3 смотрим, нужнается ли в удовлетворении на текущем тике и есть ли деньги
                if (need.ShouldTryToSatisfy() == false) { continue; } else if (worker.HasEnoughMoney(need.Cost) == false) { continue; }

                bool result = need.TryToSaticfy(worker, resourceList);
                if (result == false) 
                {
                    //если не получилось удовлетворить нужду и она критическая, лояльность падает
                    //тогда сразу переходим к следующей нужде
                    if (need.IsCritical == true) { worker.ChangePersonalLoyality(need.LoyalityAmount); continue; }

                }
                if (result == true)
                {   //берем деньги
                    worker.ChangeMoneyAmount(need.Cost);
                    
                    //удовлетовряем некритиеские нужды и лояльность растет
                    if (need.IsCritical == false)
                    {
                        worker.ChangePersonalLoyality(need.LoyalityAmount);
                    }
                    
                }                 
            }
            
        }

        //public void RecalculateNeedsState(List<Resource>resourceList)
        //{
        //foreach (Need need in workerNeeds)                // Проверяем каждую потребность
        //{
        //    if (need.IsCritical && need.AmountIsMoreThanOne())
        //    {
        //        IsAlive = false;// Интерпретация состояния - если значение кол-ва крит. нужды превысило единицу, работник умер
        //        return;
        //    }

        //    //if()//если работник имеет рабочее место, выдаем ему зп из ресурсов мира
        //    //если работник не умер, удволетврояем все нужды за деньги
        //    if (need.AmountIsMoreThanOne() == false )
        //    {//& need.AmountIsMoreThanMinus0_1() == true

        //        if (HasEnoughMoney(need.Cost)==true)//убеждаемся, что денег хватает
        //        {   
        //            bool isConfirmed=need.ChangePerTick(resourceList);
        //            if (isConfirmed)
        //            {
        //                ChangeMoneyAmount(-need.Cost);

        //                // бонус только за небазовые нужды
        //                if (need.IsCritical == false)
        //                {
        //                    ChangePersonalLoyality(need.LoyalityAmount);
        //                }
        //            }
        //            else
        //            {
        //                // штраф только за базовые нужды
        //                if (need.IsCritical)
        //                {
        //                    ChangePersonalLoyality(need.LoyalityAmount);
        //                }
        //            }

        //        }
        //    }
        //}
        //}


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
            PersonalMoney = PersonalMoney + value;
        }

        public void Tick(int standartSalary,List<Resource>resourceList)
        {
            
            //RecalculateSalary(standartSalary);
            RecalculateNeedsState(this,resourceList);
                        
        }

        //public void RecalculateSalary(int standartSalary)
        //{   if (WorkPlaceId != -1 && WorkPlaceId != null)
        //    {
        //        ChangeMoneyAmount(standartSalary);
        //    }
        //}

        //новый обобщенный метод ищет нужный элемент в подаваемой на вход коллекции
        //аналог public Need GetNeed(Type type)
        //{
        //    return workerNeeds.FirstOrDefault(x => x.GetType() == type);
        //}
        public T GetNeed<T>() where T : Need
        {
            return workerNeeds.OfType<T>().FirstOrDefault();
        }





    }
}
