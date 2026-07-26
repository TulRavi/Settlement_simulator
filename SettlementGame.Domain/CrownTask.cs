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
        public double LoyalityCounter { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        private GameDbContext _dbContext;

        
        //private GameDbContext _dbContext;
        //public GameDbContext DbContext
        //{
        //    get => _dbContext;
        //    set => _dbContext = value;
        //}

        public CrownTask(Resource resource,int numberOfTicks,double loyalityCounter)
        {
            Resource = resource;               
            NumberOfTicks = numberOfTicks;     
            LoyalityCounter = loyalityCounter; 
            
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
    

