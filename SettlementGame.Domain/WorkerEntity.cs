using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class WorkerEntity
    {
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
        public int? WorkPlaceId { get; set; } // вместо Building
        public bool IsEmployed { get; set; }

        private double personalLoyality;

        public double PersonalLoyality
        {
            get { return personalLoyality; }
            set { personalLoyality = Math.Clamp(value, 0, 1); }
        }
    }
}
