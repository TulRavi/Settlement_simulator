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

            var worker = new Worker(needs);
            worker.WorkPlaceId = (int)entity.WorkPlaceId; 
            //worker.WorkPlace = null;
            worker.Id = entity.Id;
            worker.X = entity.X;
            worker.Y = entity.Y;
            worker.IsAlive = entity.IsAlive;
            worker.PersonalLoyality = entity.PersonalLoyality;
            
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
                PersonalLoyality = domainWorker.PersonalLoyality
            };
        }
    }
}
