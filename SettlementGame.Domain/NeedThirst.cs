using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class NeedThirst:Need
    {
        public override bool IsCritical
        {
            get { return true; }
        }

        public override int Cost { get; set; } = 0;
        public override double LoyalityAmount
        {
            get { return -0.1; }
        }

        public override bool ChangePerTick(DataWorld world)
        {
                        ChangeAmount(0.2);

            foreach (AnyResource water in world.SettlementResourceList)
            {
                if (water.ResourceCategory==ResourceCategory.Water && water.TryToConsume(3))
                {
                    ChangeAmount(-0.3);
                    return true;
                }

            }
            return false;
        }
        
    }
}
