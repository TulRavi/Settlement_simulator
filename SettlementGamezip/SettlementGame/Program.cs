using SettlementGame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using static System.Collections.Specialized.BitVector32;
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
            Console.WriteLine("Enter the number of workers");
            int numberOfWorkers=int.Parse(Console.ReadLine());
            DataWorld world=WorldCreator.CretateWorld();
            world.WorkerEmploymentService=WorldCreator.CreateWorkerEmploymentService(world);
            WorldCreator.CreateDateTime(world);
            //world.WorkersList = new List<Worker>();
            WorldCreator.CreateWorkers(numberOfWorkers, world);
            
            Console.WriteLine("workers with needs were created");
            //CreateResourses();
            //Data.WorkersList.ElementAt(0).workerNeeds.ElementAt(0).ChangePerTick(0.1);
            //Data.WorkersList.ElementAt(0).workerNeeds.ElementAt(1).ChangePerTick(0.2);
            //Data.WorkersList.ElementAt(3).workerNeeds.ElementAt(1).ChangePerTick(0.5);
            
            string userInput = null;
            while(userInput!= "end")
            {

                //Console.WriteLine("Enter end to finish the simulation,1 for create smth,2 to manage workers");
                //userInput = Console.ReadLine();
                //if (userInput == "1") {
                //    WorldCreator.CreateNewBuilding(world);

                //    //WorldCreator.CreateBuilding(world, BuildingType.WoodMakery);
                //}
                
                Array values = Enum.GetValues(typeof(UsersActionType));

                foreach (UsersActionType value in values)
                {
                    Console.WriteLine($"{(int)value} - {value}");
                }
                int choice = int.Parse(Console.ReadLine());
                UsersActionType actionType = (UsersActionType)choice;

                IUserAction action = null;
                if (actionType == UsersActionType.End)
                {
                    userInput = "end";
                }
                    if (actionType == UsersActionType.CreateBuilding)
                {
                    WorldCreator.PrintAllPossibleBuildings(world);
                    Console.WriteLine("Type building name:");

                    string buildingInput = Console.ReadLine();

                    if (!Enum.TryParse(buildingInput, true, out BuildingType buildingType))
                        return;

                    CreateBuildingContext context = new CreateBuildingContext(buildingType);

                    action = UsersActionsCatalog.CreateBuildingAction(context);
                }
                else if (actionType == UsersActionType.Destroybuilding)
                { //
                    WorldCreator.PrintBuildedBuildings(world);
                    Console.WriteLine("Type building index:");
                    int buildingIndex = int.Parse(Console.ReadLine());

                    DestroyBuildingContext context = new DestroyBuildingContext(world.BuildingList.ElementAt(buildingIndex));

                    action = UsersActionsCatalog.DestroyBuildingAction(context);
                }

                else if (actionType == UsersActionType.HireWorker)
                {
                    WorldCreator.PrintBuildedBuildings(world);
                    Console.WriteLine("Type building index:");
                    int buildingIndex = int.Parse(Console.ReadLine());

                    WorldCreator.PrintWorkersList(world);
                    Console.WriteLine("Type worker index:");
                    int workerIndex = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select the shift:1/2/3");
                    int workerShift = int.Parse(Console.ReadLine());
                    TimeSpan startWorkingTime1=new TimeSpan();
                    TimeSpan endWorkingTime1 = new TimeSpan();
                    if (workerShift == 1) 
                    {
                        startWorkingTime1 = new TimeSpan(08, 00, 01);
                        endWorkingTime1 = new TimeSpan(15, 59, 00);
                    }
                    if (workerShift == 2)
                    {
                        startWorkingTime1 = new TimeSpan(16, 00, 01);
                        endWorkingTime1 = new TimeSpan(23, 59, 00);
                    }
                    if (workerShift == 3)
                    {
                        startWorkingTime1 = new TimeSpan(00, 00, 01);
                        endWorkingTime1 = new TimeSpan(08, 00, 00);
                    }
                    //else
                    //{
                    //   startWorkingTime = new TimeSpan(08, 00, 01);
                    //     endWorkingTime = new TimeSpan(16, 00, 00);
                    //}
                    HireWorkerContext context =
                            new HireWorkerContext(workerIndex, buildingIndex, startWorkingTime1, endWorkingTime1);

                    action = UsersActionsCatalog.HireWorkerAction(context);
                }

                else if (actionType == UsersActionType.FireWorker)
                {
                    //WorldCreator.PrintBuildedBuildings(world);
                    //Console.WriteLine("Type building index:");
                    //int buildingIndex = int.Parse(Console.ReadLine());

                    WorldCreator.PrintHiredWorkersList(world);
                    Console.WriteLine("Type worker index:");
                    int workerIndex = int.Parse(Console.ReadLine());
                    int buildingIndex = world.BuildingList.IndexOf(world.WorkersList.ElementAt(workerIndex).WorkPlace);
                    FireWorkerContext context =
                        new FireWorkerContext(workerIndex, buildingIndex);

                    action = UsersActionsCatalog.FireWorkerAction(context);
                }

                else if (actionType == UsersActionType.DoNothing)
                {
                    
                }
                if (action != null)
                {
                    action.Execute(world);
                }

                Tick(world);
                WorldCreator.PrintState(world);
            }
        }
        
        //public static void DateChanges(DataWorld world)
        //{
        //    currentDate=currentDate.AddDays(1);
        //}
        
        //public static void TempFireWorkerDirectly(DataWorld world,Building building,Worker worker)
        //{
        //    IUserAction action = null;
            
        //    int buildingIndex = world.BuildingList.IndexOf(building);

        //    int workerIndex = world.WorkersList.IndexOf(worker);

        //    FireWorkerContext context =
        //        new FireWorkerContext(workerIndex, buildingIndex);

        //    action = UsersActionsCatalog.FireWorkerAction(context);
        //    action.Execute(world);
        //}
        
        public static void Tick(DataWorld world)
        {
            world.GameTime=world.GameTime.AddHours(1);
            world.tempHoursCounter++;

            //ChangeNeedsAmount(world);
            if (world.tempHoursCounter == 8)
            { 
                foreach (Worker worker in world.WorkersList)
                {
                    foreach (Need need in worker.workerNeeds)
                    {
                        need.ChangePerTick(world);
                    }
                }
                world.tempHoursCounter = 0;
            }

            foreach (Building building in world.BuildingList)
            {   
                
                if (building.HasEmployee == true&& building.AssignedWorker.StartWorkingTime<=world.GameTime.TimeOfDay&& world.GameTime.TimeOfDay <= building.AssignedWorker.EndWorkingTime)
                {
                    building.CreateSomething(world);
                }
            }
            RemoveDeadWorkers(world);
            


        }

        public static void RemoveDeadWorkers(DataWorld world) //todo: перенести в другой класс
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
    

