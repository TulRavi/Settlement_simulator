using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class Resource
    {
        public ResourceType ResourceType { get;}
        public ResourceCategory ResourceCategory {get;}
        
        protected int amount;
        protected int Amount
        {
            get { return amount; }
            set { amount = Math.Clamp(value, 0, 1000); }
        }


        //protected void ChangeResourseAmount(int delta)
        //{
        //    Amount = Amount + delta;
        //}

        public Resource(ResourceType ResourceType, int Amount)
        {
            this.ResourceType = ResourceType;
            ResourceCategory = ResourceCatalog.GetCategory(ResourceType);
            this.Amount = Amount;

        }
        //public abstract void ChangePerTick(DataWorld world);
    }
}
