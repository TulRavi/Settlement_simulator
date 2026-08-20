using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    internal class NeedSalary:Need
    {
        public NeedSalary()
        {
        }

        public NeedSalary(double amount)
        {
            Amount = amount;
        }
        public override bool IsCritical
        {
            get { return false; }
        }

        public override int Cost { get; set; } = 0;
        public override double LoyaltyAmount
        {
            get { return -0.1; }
        }
        public override double GetIncreaseChangePerTick()
        {
            return 1;
        }

        public override bool TryToSatisfy(Worker worker, List<Resource> resourceList)
        {
            Resource selectedResource = resourceList.Find(x => x.ResourceType == ResourceType.Coin);
            bool isPossibleToConsume=selectedResource.TryToConsume((int)worker.CurrentSalary);
            if (isPossibleToConsume == true) { return true; } else { return false; }
            
        }
    }
}
