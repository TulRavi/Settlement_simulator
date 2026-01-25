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
            world.ResourceList.Add(new Resource(ResourceType.Meat, 10));
            world.ResourceList.Add(new Resource(ResourceType.Berries, 20));
            world.ResourceList.Add(new Resource(ResourceType.CleanWater, 50));
            world.ResourceList.Add(new Resource(ResourceType.Wood, 40));
            world.ResourceList.Add(new Resource(ResourceType.Stone, 40));
            world.ResourceList.Add(new Resource(ResourceType.Gold, 0));

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
    }
}
