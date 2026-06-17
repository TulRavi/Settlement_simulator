using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class AnyResource
    {
        public ResourceType ResourceType { get; set; } //добавили set для создаия ресурса при создании мира
        public ResourceCategory ResourceCategory {get;}
        
        protected int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = Math.Clamp(value, 0, 1000); } //убрали протектид для создаия ресурса при создании мира
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
        public void Decrease(int value)
        {
            amount = amount - value;
        }


        public AnyResource(ResourceType ResourceType, int Amount)
        {
            this.ResourceType = ResourceType;
            ResourceCategory = ResourceCatalog.GetCategory(ResourceType);
            this.Amount = Amount;

        }
        //public abstract void ChangePerTick(DataWorld world);
    }
}
