using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Worker
    {
        //public double BasicNeeds { get; private set; }
        //public double Thirst { get; set; }

        List<Need>wokerNeeds=new List<Need>();

        public Worker(List<Need> wokerNeeds)
        {
            this.wokerNeeds = wokerNeeds;
        }
        public Worker()
        {
            
        }


        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        //сюда добавляем нужны. лучше создать отдельный лист нужд мб?

        //List <double> BasicNeeds;




        //public bool IsCitizen { get; private set; }






    }
}
