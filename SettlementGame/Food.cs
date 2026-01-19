using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal abstract class Food : Resourse
    {
        //List<Food> foodList = new List<Food>();
        public static List <Food> foodList { get; set; }
        public static Random random = new Random();

        public static int foodAmount;
        public static int FoodAmount
        {
            get { return foodAmount; }
            private set { foodAmount = Math.Clamp(value, 0, 1000); }
        }
        public static void ChangeFoodAmount(int delta)
        {
            FoodAmount = FoodAmount + delta;
        }
        //public Food(double amount)
        //{
        //    this.amount = Amount;
        //}

    }
}
