using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Resourses
    {//todo продумать не сделать ли отд.классы для ресурсов. например, ягоды = еда и рабочий веган хочет съесть их или грибы.
        private int wood;
        public int Wood
        {
            get { return wood; }
            set { wood = Math.Clamp(value, 0, 1000); }
        }
        private int water;
        public int Water
        {
            get { return water; }
            set { water = Math.Clamp(value, 0, 1000); }
        }
        private static int food;
        public static int Food
        {
            get { return food; }
            set { food = Math.Clamp(value, 0, 1000); }
        }
    }
}
