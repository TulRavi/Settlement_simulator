using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class Resource
    {
        public ResourceType ResourceType { get; set; } //set was added to allow creating resources when creating the world
        public ResourceCategory ResourceCategory { get; }

        protected int amount;

        public int Amount
        {
            get { return amount; }
            set { amount = Math.Clamp(value, 0, 1000); } //protected was removed to allow creating resources when creating the world
        }


        //Try to consume the specified amount. Returns false if there are not enough resources.
        public bool TryToConsume(int value)
        {
            if (amount < value)
                return false;

            amount = amount - value;
            return true;
        }

        public void Increase(int value)
        {
            Amount = Amount + value;
        }

        public void Decrease(int value)
        {
            Amount = Amount - value;
        }


        public Resource(ResourceType ResourceType, int Amount)
        {
            this.ResourceType = ResourceType;
            ResourceCategory = ResourceCatalog.GetCategory(ResourceType);
            this.Amount = Amount;
        }
    }
}