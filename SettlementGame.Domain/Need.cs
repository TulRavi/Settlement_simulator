using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Each need should be able to live through one Tick by itself.
External code should not know:
what kind of need it is,
how it increases,
how it is satisfied,
or what resources it consumes.

External code should always do the same thing:
"Live through a Tick."
*/

namespace SettlementGame.Domain
{
    public abstract class Need
    {
        private double amount;

        public double Amount
        {
            get { return amount; }

            //The amount of a need is always kept between 0 and 1.
            protected set { amount = Math.Clamp(value, 0, 1); }
        }

        public abstract int Cost { get; set; }
        public abstract double LoyaltyAmount { get; }
        public abstract bool IsCritical { get; }

        public void ChangeAmount(double delta)
        {
            Amount = Amount + delta;
        }

        public void Increase()
        {
            ChangeAmount(GetIncreaseChangePerTick());
        }

        public abstract double GetIncreaseChangePerTick();

        public abstract bool TryToSatisfy(Worker worker, List<Resource> resourceList);

        public virtual bool ShouldTryToSatisfy() //virtual instead of abstract, so it does not have to be overridden without a reason
                                                 //by default there is no need to satisfy it, specific needs can define their own condition
        {
            return Amount > 1;
        }
    }
}