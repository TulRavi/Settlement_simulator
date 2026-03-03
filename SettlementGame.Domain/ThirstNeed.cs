using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class ThirstNeed:Need
    {
        public override bool IsCritical
        {
            get { return true; }
        }

        public override void ChangePerTick(DataWorld world)
        {
            ChangeAmount(0.2);

            foreach (ResourceOfSettlement water in world.ResourceList)
            {
                if (water.ResourceCategory==ResourceCategory.Water && water.TryToConsume(3))
                {
                    ChangeAmount(-0.3);
                    break;
                }
            }
        }
        
    }
}
