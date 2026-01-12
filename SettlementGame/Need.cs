using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Need
    {
        public string NeedName { get; set; }

        private double needAmount;

        public bool IsCritical { get; private set; }

        public double NeedAmount
        {
            get { return needAmount; }
            set {needAmount = Math.Clamp(value, 0, 1);}
        }

        public Need(string NeedName,double NeedAmount=0,bool IsCritical=false) {
            this.NeedName = NeedName;
            this.NeedAmount = NeedAmount;
            this.IsCritical = IsCritical;
            }



        //private double thirst;

        //public double Thirst
        //{
        //    get { return thirst; }
        //    set
        //    {
        //        thirst = Math.Clamp(value, 0, 1);
        //        if (thirst > 1) { Worker.IsAlive = false; }
        //    }
        //}


        //private double hunger;

        //public double Hunger
        //{
        //    get { return hunger; }
        //    set
        //    {
        //        hunger = hunger = Math.Clamp(value, 0, 1);
        //        if (hunger > 1) { IsAlive = false; }
        //    }
        //}
    }
}
