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
        //public override double delta
        //{
        //    get { return 0.2; }
        //}
    }
}
