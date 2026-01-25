using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class HungerNeed : Need
    {
        public override bool IsCritical
        {
            get { return true; }
        }

        public override void ChangePerTick(DataWorld world)
        {
            ChangeAmount(0.1);

            foreach (Resource food in world.ResourceList)
            {
                if (food.TryToConsume(1))
                {
                    ChangeAmount(-0.2);
                    break;
                }
            }
        }

        //set => throw new NotImplementedException(); }

        //    public override double delta
        //    {   if(Resourses.Food>=1){
        //        Resourses.Food=Resourses.Food-1;
        //        get{return 0.0}

        //}else
        //get { return 0.1; }
        //    }


    }
}
