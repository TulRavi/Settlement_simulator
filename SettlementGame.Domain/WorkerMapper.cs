using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class WorkerMapper
    {

        public static Worker ToDomain(WorkerEntity entity)
        {   
            var needs = Worker.CreateDefaultNeeds(); // временно?

            //var worker = new Worker(needs);
           
            Worker worker = new Worker(new List<Need>
{
    new NeedHunger(entity.Hunger),
    new NeedThirst(entity.Thirst),
    new NeedAlcohol(entity.Alcohol),
    new NeedSalary(entity.Salary),
});
            worker.WorkPlaceId = entity.WorkPlaceId; 
            //worker.WorkPlace = null;
            worker.Id = entity.Id;
            worker.X = entity.X;
            worker.Y = entity.Y;
            worker.IsAlive = entity.IsAlive;
            worker.PersonalLoyalty = entity.PersonalLoyalty;
            worker.CurrentSalary = entity.CurrentSalary;
                worker.PersonalMoney = entity.PersonalMoney;
            //worker.info = entity.info;


            return worker;
        }
        public static WorkerEntity ToEntity(Worker domainWorker)
        {

            //domainWorker.WorkPlace = null;

            return new WorkerEntity
            {

                X = domainWorker.X,
                Y = domainWorker.Y,
                //Id = domainWorker.Id,
                IsAlive = domainWorker.IsAlive,
                WorkPlaceId = domainWorker.WorkPlaceId,
                IsEmployed = domainWorker.IsEmployed,
                PersonalLoyalty = domainWorker.PersonalLoyalty,
                PersonalMoney = domainWorker.PersonalMoney,
                CurrentSalary = domainWorker.CurrentSalary,
                info = domainWorker.info,
                Hunger = domainWorker.GetNeed<NeedHunger>().Amount,
                Thirst = domainWorker.GetNeed<NeedThirst>().Amount,
                Alcohol = domainWorker.GetNeed<NeedAlcohol>().Amount,
                Salary = domainWorker.GetNeed<NeedSalary>().Amount,
            };
        }
    }
}
