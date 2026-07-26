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

        public override bool TryToSaticfy(Worker worker, List<Resource> resourceList)
        {
            foreach (Resource food in resourceList)
            {
                if (food.ResourceCategory == ResourceCategory.Food && food.TryToConsume(1)) //нужда отправляет запрос на потребление ресурсу
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

        public override int Cost { get; set; } = 1;
        private double _loyalityAmount;

        public override double LoyalityAmount
        {
            get { return -0.05; }
        }

        //public override bool ChangePerTick(List<Resource> resourceList) //
        //{
        //    ChangeAmount(0.1);

        //    foreach (Resource food in resourceList)
        //    {
        //        if (food.ResourceCategory == ResourceCategory.Food && food.TryToConsume(1)) //нужда отправляет запрос на потребление ресурсу
        //        {
        //            ChangeAmount(-0.2);
        //            return true;
        //        }

        //    }
        //    return false;
        //}

        //set => throw new NotImplementedException(); }

        //    public override double delta
        //    {   if(resources.Food>=1){
        //        resources.Food=resources.Food-1;
        //        get{return 0.0}

        //}else
        //get { return 0.1; }
        //    }


    }
}
