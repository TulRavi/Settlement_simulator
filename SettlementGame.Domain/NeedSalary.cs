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

        public override int Cost { get; set; } = 30;
        public override double LoyalityAmount
        {
            get { return -0.1; }
        }
        public override double GetIncreaseChangePerTick()
        {
            return 0.1;
        }

        public override bool TryToSaticfy(Worker worker, List<Resource> resourceList)
        {
            foreach (Resource moneta in resourceList)
            {
                if (moneta.ResourceCategory == ResourceCategory.Money && moneta.TryToConsume(Cost))
                {
                    ChangeAmount(-0.1);
                    return true;
                }
            }
            return false;
        }
    }
}
