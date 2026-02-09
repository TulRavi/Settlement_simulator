using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal static class WorldCreator
    {
        public static DataWorld CretateWorld()

        {   //List <Resource> listResource=new List <Resource>();
            DataWorld world = new DataWorld();
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Meat, 100));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Berries, 202));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.CleanWater, 500));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Wood, 40));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Stone, 40));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Gold, 10));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Doska, 0));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Kirpich, 0));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Moneta, 100));
            AddPossibleBuildings(world);
            return world;
        }

        public static WorkerEmploymentService CreateWorkerEmploymentService(DataWorld world)
        {
            WorkerEmploymentService workerEmploymentService = new WorkerEmploymentService();
            return workerEmploymentService;
        }
        public static void AddPossibleBuildings(DataWorld world)
        {
            Array buildingTypes = Enum.GetValues(typeof(BuildingType));

            foreach (BuildingType building in buildingTypes)
            {
                world.PossibleBuildingList.Add(building);
            }
        }
        public static void CreateWorkers(int numberOfWorkers, DataWorld world)
        {


            for (int i = 0; i < numberOfWorkers; i++)
            { //Потребности добавлены в лист потребностей
                List<Need> workerNeeds = new List<Need>();
                workerNeeds.Add(new HungerNeed());
                workerNeeds.Add(new ThirstNeed());
                Worker worker = new Worker(workerNeeds);//создан рабочий с заданными потребностями
                worker.IsAlive = true;
                world.WorkersList.Add(worker);//рабочий с заданнами потербностями добавлен в лист рабочих
                //workerNeeds.Clear();
            }
        }
        public static void PrintState(DataWorld world)
        {
            int i = 0;
            foreach (Worker worker in world.WorkersList)
            {


                foreach (Need need in worker.workerNeeds)
                {
                    Console.WriteLine($"{worker} {i} {need.ToString()} {need.Amount}");
                }
                i++;
            }
            i = 0;
            foreach (ResourceOfSettlement resource in world.ResourceList)
            {


                Console.WriteLine($"{resource} {resource.ResourceType.ToString()} {resource.Amount}");


            }
        }

        public static void PrintBuildedBuildings(DataWorld world)
        {
            foreach (Building building in world.BuildingList)
            {
                
                    Console.WriteLine($"{building.GetType().ToString()} index of building:{world.BuildingList.IndexOf(building)} has Employee {building.HasEmployee}");
                
            }
        }
        //public static void CreateNewBuilding(DataWorld world)
        //{
        //    WorldCreator.PrintAllPossibleBuildings(world);
        //    Console.WriteLine("Type the name of a building");
        //    String userInput = Console.ReadLine();
        //    // Попытка преобразовать строку в enum (игнорируя регистр)
        //    if (Enum.TryParse<BuildingType>(userInput, true, out BuildingType result))
        //    {
        //        // Получение точного имени из enum
        //        string exactName = result.ToString();
        //        CreateBuildingContext createBuildingContext = new CreateBuildingContext(result);
        //        CreateBuildingAction createBuildingAction = new CreateBuildingAction(createBuildingContext);
        //        createBuildingAction.Execute(world);
        //        //Console.WriteLine($"Введенное имя: {exactName}"); // Выведет: Monday
        //    }
        //    else
        //    {
        //        Console.WriteLine("No such word in enum");
        //    }
        //}

        public static void PrintWorkersList(DataWorld world)
        {
            foreach (Worker worker in world.WorkersList)
            {
                Console.WriteLine($"index of a worker:{world.WorkersList.IndexOf(worker)} if employeed {worker.IsEmployed}");
            }
        }

        public static void PrintHiredWorkersList(DataWorld world)
        {
            foreach (Worker worker in world.WorkersList)
            {
                if (worker.IsEmployed == true)
                {
                    Console.WriteLine($"index of a worker:{world.WorkersList.IndexOf(worker)} is working in {worker.WorkPlace.BuildingType.ToString()} {world.BuildingList.IndexOf(worker.WorkPlace)}");
                }
            }
        }


        //public static void ManageWorkers(DataWorld world)
        //{
        //    Console.WriteLine("Type 1 to hire a worker,2 to fire, end to finish");
        //    String userInput = Console.ReadLine();
        //    while (userInput != "end")
        //    {
        //        if (userInput == "1")//тут нужно присвоить рабочему место.1)
        //                             //PrintBuildedBuildings(world)
        //                             //
        //                             //показываем лист рабочих со статусом(уже есть метод))
        //                             //присваиваем каждому рабочему номер и даем юзеру его ввести
        //                             //связываем рабочего с местом(добавить метод в класс Worker)
        //                             //
        //        {
        //            Console.WriteLine("Please type an index of a building");
        //            PrintBuildedBuildings(world);
        //            userInput = Console.ReadLine();
        //            int indexOfBuilding = int.Parse(userInput);
        //            PrintWorkersList(world);
        //            Console.WriteLine("Please type an index of a worker");
        //            userInput = Console.ReadLine();
        //            int indexOfWorker = int.Parse(userInput);
        //            TryAssignWorker(world, world.WorkersList.ElementAt(indexOfWorker), world.BuildingList.ElementAt(indexOfBuilding));

        //        }

        //    }
        //}

        public static bool TryAssignWorker(DataWorld world, Worker worker, Building building)
        {
            if (worker.IsEmployed)
                return false;

            if (building.HasEmployee)
                return false;

            worker.AssignWithWorkPlace(building);
            building.AssignWorker(worker);

            return true;
        }

        public static void UnassignWorker(Worker worker)
        {
            if (worker.WorkPlace == null)
                return;

            Building building = worker.WorkPlace;

            worker.UnassignWithWorkPlace();
            building.RemoveWorker();
        }

        public static void PrintAllPossibleBuildings(DataWorld world)
        {
            foreach (BuildingType building in world.PossibleBuildingList)
            {
                    Console.WriteLine($"{building.ToString()}");
            }


        }
    }
}
