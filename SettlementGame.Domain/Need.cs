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
            protected set { amount = Math.Clamp(value, 0, 1); }
        }
        public abstract int Cost { get; set; }
        public abstract double LoyalityAmount { get; }
        public abstract bool IsCritical { get; }
        //public abstract double delta { get; }
        public void ChangeAmount(double delta)
        {
            Amount = Amount + delta;
        }
        //public abstract bool ChangePerTick(List<Resource> resourceList);//его будем переопределять
        //public abstract bool ChangePerTick(List<Resource> resourceList, int salary);

        public void Increase()
        {
            ChangeAmount(GetIncreaseChangePerTick());
        }
        public abstract double GetIncreaseChangePerTick();

        public abstract bool TryToSaticfy(Worker worker, List<Resource> resourceList);
        public virtual bool ShouldTryToSatisfy() //виртуал, а не абстракат, чтобы не переопределять без необходимости
            //априори нужды не существует, для базовых она будет >0.5, для небазовых по мере наличия потребности(?)
        {
            return Amount > 1;
        }
        //public abstract bool ShouldTryToSaticfy();
        //public bool AmountIsMoreThanOne()
        //{
        //    return Amount >= 1;                       
        //}
        //public bool AmountIsMoreThanMinus0_1()
        //{
        //    return Amount >= -0.1;
        //}


    }
}
