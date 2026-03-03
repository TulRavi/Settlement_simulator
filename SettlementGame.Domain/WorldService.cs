using System;
using System.Collections.Generic;
using System.Text;


namespace SettlementGame.Domain
{   //CreateWorld

//GetWorld

//ResetWorld
    public static class WorldService
    {
    public static DataWorld CreateWorld(DataWorld world,int numberOfWorkers=3)
    {   
        //DataWorld world = new DataWorld();
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Meat, 100));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Berries, 202));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.CleanWater, 500));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Wood, 40));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Stone, 40));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Gold, 10));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Doska, 0));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Kirpich, 0));
        world.ResourceList.Add(new ResourceOfSettlement(ResourceType.Moneta, 100));
        
        WorldCreator.AddPossibleBuildings(world);

        WorldCreator.CreateDateTime(world);
        
        world.workerEmploymentService = WorldCreator.CreateWorkerEmploymentService(world);
        
        WorldCreator.CreateWorkers(numberOfWorkers, world);

        return world;
    }

        public class WorkerDto
        {
            public int Id { get; set; }

            public bool IsAlive { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            //public TimeSpan StartWorkingTime { get; set; }
            //public TimeSpan EndWorkingTime { get; set; }
        }
        public static List<WorkerDto> GetWorkerDtoList(DataWorld world)
        {
            List<WorkerDto> workerDtoList = new List<WorkerDto>();
            foreach (Worker worker in world.WorkersList)
            {
                WorkerDto workerDto = new WorkerDto();
                workerDto.Id = worker.Id;
                workerDto.X = worker.X;
                workerDto.Y = worker.Y;
                workerDto.IsAlive = worker.IsAlive;
                workerDtoList.Add(workerDto);
            }
            return workerDtoList;
        }

        public static void CreateWorkersByService(int numberOfWorkers, DataWorld world)
        {
            WorldCreator.CreateWorkers(numberOfWorkers, world);
        }

        
        //public static DataWorld GetWorld()
        //{
        //    return SettlementGame.Web.GameContoller.world;

        //}

    }
}
