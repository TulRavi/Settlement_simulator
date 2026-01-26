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
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Meat, 10));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Berries, 20));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.CleanWater, 50));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Wood, 40));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Stone, 40));
            world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Gold, 0));

            return world;
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
        public static void CreateBuilding(DataWorld world, BuildingType buildingType)
        {
            Building building = new Building(buildingType);
            world.BuildingList.Add(building);
        }
    }
}
