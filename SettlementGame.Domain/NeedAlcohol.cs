using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class NeedAlcohol : Need
    {
        public NeedAlcohol()
        {
        }

        public NeedAlcohol(double amount)
        {
            Amount = amount;
        }

        public override int Cost { get; set; } = 5;

        public override bool IsCritical
        {
            get { return false; }
        }

        public override double LoyaltyAmount
        {
            get { return +0.05; }
        }

        //The need increases by 0.1 on each Tick.
        public override double GetIncreaseChangePerTick()
        {
            return 0.1;
        }

        public override bool TryToSatisfy(Worker worker, List<Resource> resourceList)
        {
            foreach (Resource alcohol in resourceList)
            {
                if (alcohol.ResourceCategory == ResourceCategory.Alcohol && alcohol.TryToConsume(1)) //the need tries to consume a resource
                {
                    ChangeAmount(-0.3);
                    //Need to add worker expenses here. Probably this should be a Worker method, and the need should not be satisfied if the worker has no money.
                    return true;
                }
            }

            return false;
        }
    }
}