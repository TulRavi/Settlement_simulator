using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal abstract class Resource1
    {

        public int amount;
        public int Amount
        {
            get { return amount; }
            set { amount = Math.Clamp(value, 0, 1000); }
        }
        public static void ChangeResourseAmount(Resource1 resourse, int delta)
        {
            resourse.Amount = resourse.Amount + delta;
        }
    }
}
