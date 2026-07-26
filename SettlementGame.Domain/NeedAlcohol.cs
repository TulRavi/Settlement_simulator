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

        //public override double LoyalityAmount { get => LoyalityAmount; set => throw new NotImplementedException(); }
        public override double LoyalityAmount
        {
            get { return +0.05; }
        }

        public override double GetIncreaseChangePerTick()
        {
            return 0.1;
        }
        public override bool TryToSaticfy(Worker worker, List<Resource> resourceList) //
        {
            foreach (Resource alcohol in resourceList)
            {
                if (alcohol.ResourceCategory == ResourceCategory.Alcohol && alcohol.TryToConsume(1)) //нужда отправляет запрос на потребление ресурсу
                {
                    ChangeAmount(-0.3);
                    //куда вписать затраты рабочего. видимо, это дб метод рабочего и нужна не должна удовлетовряться, если у него нет денег.
                    return true;
                }
            }
            return false;
        }
    }
}
