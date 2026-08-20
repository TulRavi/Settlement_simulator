using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class NeedThirst:Need
    {
        public NeedThirst()
        {
        }

        public NeedThirst(double amount)
        {
            Amount = amount;
        }
        public override bool IsCritical
        {
            get { return true; }
        }

        public override int Cost { get; set; } = 0;
        public override double LoyaltyAmount
        {
            get { return -0.1; }
        }
        public override double GetIncreaseChangePerTick()
        {
            return 0.2;
        }
        public override bool ShouldTryToSatisfy()
        {
            if (Amount > 0) { return true; } else { return false; }
        }

        public override bool TryToSatisfy(Worker worker, List<Resource> resourceList)
        {
            foreach (Resource water in resourceList)
            {
                if (water.ResourceCategory == ResourceCategory.Water && water.TryToConsume(3))
                {
                    ChangeAmount(-0.2);
                    return true;
                }
            }
            return false;
        }

    }
}
