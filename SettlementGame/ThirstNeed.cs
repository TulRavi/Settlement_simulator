using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class ThirstNeed:Need
    {
        public override bool IsCritical
        {
            get { return true; }
        }

        public override void ChangePerTick(DataWorld world)
        {
            ChangeAmount(0.2);

            foreach (Resource water in world.ResourceList)
            {
                if (water.TryToConsume(3))
                {
                    ChangeAmount(-0.3);
                    break;
                }
            }
        }
        //public override double delta
        //{
        //    get { return 0.2; }
        //}
    }
}
