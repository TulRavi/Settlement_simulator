using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class NeedHunger : Need
    {
        public NeedHunger()
        {
        }

        public NeedHunger(double amount)
        {
            Amount = amount;
        }
        //public int Cost = 1;
        public override bool IsCritical
        {
            get { return true; }
        }

        public override double GetIncreaseChangePerTick()
        {
            return 0.2;
        }

        public override bool TryToSatisfy(Worker worker, List<Resource> resourceList)
        {
            foreach (Resource food in resourceList)
            {
                if (food.ResourceCategory == ResourceCategory.Food && food.TryToConsume(1)) //A demand sends a request to consume a resource
                {
                    ChangeAmount(-0.2);
                    return true;
                }
            }
            return false;
        }

        public override bool ShouldTryToSatisfy()
        {
            if (Amount > 0) { return true; } else { return false; }
        }

        public override int Cost { get; set; } = -1;
        private double _LoyaltyAmount;

        public override double LoyaltyAmount
        {
            get { return -0.05; }
        }



    }
}
