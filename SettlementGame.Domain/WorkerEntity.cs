using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class WorkerEntity
    {
        public double Hunger { get; set; }

        public double Thirst { get; set; }

        public double Alcohol { get; set; }

        public double Salary { get; set; }


        public int Id { get; set; }
        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int? WorkPlaceId { get; set; } // вместо Building , вопр.знак, чтобы в случае отсвутвия не записал число 0, кот.реально мб айди
        public bool IsEmployed { get; set; }

        private double personalLoyality;

        public double PersonalLoyality
        {
            get { return personalLoyality; }
            set { personalLoyality = Math.Clamp(value, 0, 1); }
        }
        public int PersonalMoney { get; set; }

        public int?CurrentSalary { get; set; }
        public string info { get; set; }
    }
}
