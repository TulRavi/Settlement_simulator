using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class ResourceOfSettlement
    {
        public ResourceType ResourceType { get;}
        public ResourceCategory ResourceCategory {get;}
        
        protected int amount;
        public int Amount
        {
            get { return amount; }
            protected set { amount = Math.Clamp(value, 0, 1000); }
        }


        public bool TryToConsume(int value)
        {
            if (amount < value)
                return false;

            amount = amount - value;
            return true;
        }

        public void Increase(int value)
        {
            amount = amount + value;
            
        }

        public ResourceOfSettlement(ResourceType ResourceType, int Amount)
        {
            this.ResourceType = ResourceType;
            ResourceCategory = ResourceCatalog.GetCategory(ResourceType);
            this.Amount = Amount;

        }
        //public abstract void ChangePerTick(DataWorld world);
    }
}
