using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Meat : Food
    {
        public Meat(int amount)
        {
            this.amount = Amount;
            Food.ChangeFoodAmount(amount);
        }
    }
}
