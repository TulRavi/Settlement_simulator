using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 Каждая потребность должна сама уметь проживать один тик.
Внешний код не должен знать:какая это потребность,как она растёт,как она насыщается,какие ресурсы она потребляет
Внешний код должен делть одно и то же всегда:«Проживи тик».
 * */
namespace SettlementGame.Domain
{
    public abstract class Need
    {
        private double amount;

        public double Amount
        {
            get { return amount; }                      
            private set { amount = Math.Clamp(value, 0, 1); }
        }
        public abstract int Cost { get; set; }
        public abstract double LoyalityAmount { get; set; }
        public abstract bool IsCritical { get; }
        //public abstract double delta { get; }
        public void ChangeAmount(double delta)
        {
            Amount = Amount + delta;
        }
        public abstract bool ChangePerTick(DataWorld world);//его будем переопределять
        public bool AmountIsMoreThanOne()
        {
            return Amount >= 1;                       
        }
        

    }
}
