using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class CrownTask
    {
        public Resource Resource { get; set; }
        public int NumberOfTicks { get; set; }
        public double LoyaltyCounter { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        private GameDbContext _dbContext;


        public CrownTask(Resource resource,int numberOfTicks,double LoyaltyCounter)
        {
            Resource = resource;               
            NumberOfTicks = numberOfTicks;     
            LoyaltyCounter = LoyaltyCounter; 
            
        }
        public CrownTask() { }

        public bool IsCompleted(DataWorld world)
        {
            //AnyResource reqAnyResource = world.CurrentCrownTask.Resource;
            Resource reqAnyResource = Resource;
            if (world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount >= reqAnyResource.Amount) {
                //world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount = world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount - reqAnyResource.Amount;
                return true;}
            else { return false; }
        }
        } }
    

