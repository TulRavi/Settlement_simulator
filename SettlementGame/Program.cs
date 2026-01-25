using SettlementGame;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
//Tick отвечает есть ли еда,кому дать еду,сколько дать,кому не хватило
//Worker отвечает как интерпретировать состояние потребностей
//Различия потребностей — это данные и реакции, а не доступ к складу
//HungerNeed и пр. могут иметь свой базовый рост, но кто именно(рабочий/) их удовлетворил — не их дело.
namespace SettlementGame
{
    internal class Program
    {
         
        public static DateOnly currentDate = new DateOnly();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DataWorld world=WorldCreator.CretateWorld();
            //world.WorkersList = new List<Worker>();
            WorldCreator.CreateWorkers(5,world);
            Console.WriteLine("созданы рабочие с заданными потребностями");
            //CreateResourses();
            //Data.WorkersList.ElementAt(0).workerNeeds.ElementAt(0).ChangePerTick(0.1);
            //Data.WorkersList.ElementAt(0).workerNeeds.ElementAt(1).ChangePerTick(0.2);
            //Data.WorkersList.ElementAt(3).workerNeeds.ElementAt(1).ChangePerTick(0.5);
            Console.WriteLine();
            Tick(world);


            //для теста

            //этапы: 1) старт поселения - создание рабочих начальных и приказов метополии
            //2) запуск цикла производства и изм.состояния рабочих, их кол-ва, шкал довольства игроком, кол-ва материалов/зданий
            //3)мб случайное событие - вкл.позже.




        }
        public static void DateChanges(DataWorld world)
        {
            currentDate=currentDate.AddDays(1);
        }
        public static void Tick(DataWorld world)
        {   
            DateChanges(world);
            //ChangeNeedsAmount(world);
            foreach(Worker worker in world.WorkersList)
            {
                foreach(Need need in worker.workerNeeds)
                {
                    need.ChangePerTick(world);
                }
            }
            RemoveDeadWorkers(world);

        }

        public static void RemoveDeadWorkers(DataWorld world)
        {
            foreach (var worker in world.WorkersList)
            {
                worker.RecalculateState();
            }
            //List<int> workersNumbersToDelete = new List<int>();
            //for (int w = 0; w < Data.WorkersList.Count; w++)
            //{
            //    if (Data.WorkersList.ElementAt(w).IsAlive == false) { workersNumbersToDelete.Add(w); }
            //}
            //foreach (var number in workersNumbersToDelete)
            //{
            //    Data.WorkersList.Remove(Data.WorkersList.ElementAt(number));
            //    //Data.WorkersList.ElementAt(number) = null;
            //}
            world.WorkersList.RemoveAll(n => n.IsAlive == false);
        }
        //public static void ChangeNeedsAmount(DataWorld world)//значение состояний потребностей меняется
        //{
        //    for (int i = 0; i < world.WorkersList.Count; i++)
        //    {
        //        foreach (var worker in world.WorkersList)
        //        {
        //            //if (Resourses.Food > 0)
        //            //{
        //            //    if (worker.workerNeeds.ElementAt(0).Amount >= 0.2)//если голод 0.2 и более
        //            //    {
        //            //        worker.workerNeeds.ElementAt(0).ChangePerTick(-0.2);
        //            //        Resourses.Food = Resourses.Food - 2;//рабочий съест 2 порции еды
        //            //    }
        //            //    else
        //            //    {
        //            //        worker.workerNeeds.ElementAt(0).ChangePerTick(-0.1);//если голод менее 0.2
        //            //        Resourses.Food = Resourses.Food - 1;//рабочий съест 1 порцию
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    worker.workerNeeds.ElementAt(0).ChangePerTick(0.1);
        //            //}

        //            if (world.FoodAmount > 0)
        //            {
        //                int temp;
        //                if (worker.workerNeeds.ElementAt(0).Amount >= 0.2&& Food.FoodAmount>2)//если голод 0.2 и есть 2 ед.еды
        //                {//todo добавить тут массив номеров элементов foodList,Amount которых>0; для оптимизации.
        //                 //и рандом новый у нас будет включать только значения элементов этого массива
        //                    worker.workerNeeds.ElementAt(0).ChangePerTick(DataWorld world);
        //                    int counter = 2;
        //                    while (counter > 0)
        //                    { temp = Food.foodList.Count() - 1;
        //                        if (Food.foodList.ElementAt(temp).Amount > 0)
        //                        {
        //                            Food.foodList.ElementAt(Food.random.Next(0, temp = Food.foodList.Count() - 1)).Amount += -1;
        //                            counter--;
        //                        }
        //                    }
        //                }
        //                else
        //                {   
        //                    worker.workerNeeds.ElementAt(0).ChangePerTick(-0.1);//если голод менее 0.2
        //                    int counter = 1;
        //                    while (counter > 0)
        //                    {
        //                        temp = Food.foodList.Count() - 1;
        //                        if (Food.foodList.ElementAt(temp).Amount > 0)
        //                        {
        //                            Food.foodList.ElementAt(Food.random.Next(0, temp = Food.foodList.Count() - 1)).Amount += -1;
        //                            counter--;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                worker.workerNeeds.ElementAt(0).ChangePerTick(0.1);
        //            }
        //        }

        //        }
        //    }
            
                //Worker.CheckIfWorkerIsDead();
            
        
        
        


    }
}
    

