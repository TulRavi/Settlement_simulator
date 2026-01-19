using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Berries:Food
    {
        public Berries(int amount)
        {
            this.amount = Amount;
            Food.ChangeFoodAmount(amount);
            
        }
    }
}
