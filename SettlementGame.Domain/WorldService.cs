using System;
using System.Collections.Generic;
using System.Text;


namespace SettlementGame.Domain
{   //CreateWorld

//GetWorld

//ResetWorld
    public class WorldService
    {

        private readonly DataWorld world;
        public BuildingType BuildingType { get; }


        public WorldService(DataWorld world)
        {
            this.world = world;
        }
        public DataWorld CreateWorld(int numberOfWorkers=3)
    {   
        //DataWorld world = new DataWorld();
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Meat, 100));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Berries, 202));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.CleanWater, 500));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Wood, 40));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Stone, 40));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Gold, 10));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Doska, 0));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Kirpich, 0));
        world.SettlementResourceList.Add(new AnyResource(ResourceType.Moneta, 100));
        
        WorldCreator.AddPossibleBuildings(world);

        WorldCreator.CreateDateTime(world);
        
        //world.workerEmploymentService = WorldCreator.CreateWorkerEmploymentService(world);
        
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

        public List<Building> GetBuildings()
        {
            return world.BuildingList;
        }

        public List<string> GetAvailibleBuildings()
        {

            //List<Building> AvailibleBuildingList = world.BuildingList.Where( x=> x.IsOpenedForUser == true).ToList();
            List<BuildingType> AvailibleBuildingList = world.PossibleBuildingList;
            
            Array buildingTypes = Enum.GetValues(typeof(BuildingType));

            List<string> buildingNames1 = new List<string>();
            foreach (BuildingType buildingType in Enum.GetValues(typeof(BuildingType)))
            {
                string temp = ($"Name: {buildingType}, Value: {(int)buildingType}");
                buildingNames1.Add(temp);
            }
            return buildingNames1;
        }

        public bool CreateBuilding(BuildingType buildingType) {
            CreateBuildingContext createBuildingContext = new CreateBuildingContext(buildingType);
            bool exists = Enum.IsDefined(typeof(BuildingType), createBuildingContext.BuildingType);

            {
                if (exists == true)
                {
                    CreateBuildingAction action = (CreateBuildingAction)UsersActionsCatalog.CreateBuildingAction(createBuildingContext);
                    //context.Building = world.BuildingList.Find(x => x.BuildingId == buildingId);
                    action.Execute(world);
                    return true;
                }
                else { return false; }
            }
        }
        public void RemoveBuilding(int buildingId)
        {   
            DestroyBuildingContext destroyBuildingContext = new DestroyBuildingContext(buildingId);
            DestroyBuildingAction action = (DestroyBuildingAction)UsersActionsCatalog.DestroyBuildingAction(destroyBuildingContext);
            //context.Building = world.BuildingList.Find(x => x.BuildingId == buildingId);
            action.Execute(world);
        }
        public List<WorkerDto> GetWorkerDtoList()
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

        public void CreateWorkersByService(int numberOfWorkers)
        {
            WorldCreator.CreateWorkers(numberOfWorkers, world);
        }

        public void Tick(DataWorld world) 
        {
            UpdateWorkers(world);
            UpdateBuildings(world);
            //UpdateResources();
            UpdateLoyalty(world);
        }

        private void UpdateWorkers(DataWorld world)
        {
            foreach (var worker in world.WorkersList)
            {
                worker.Tick(world);
            }
        }

        public void UpdateBuildings(DataWorld world)
        {
            foreach (var building in world.BuildingList)
            {
                building.Tick(world);
            }
        }

        private void UpdateLoyalty(DataWorld world)
        { double tempLoyality = 0;
            foreach (var worker in world.WorkersList)
            {
                tempLoyality= tempLoyality+worker.PersonalLoyality;
            }
            world.Peopleloyality= tempLoyality / world.WorkersList.Count;
            
        }

        public void UpdateGameTime(DataWorld world)
        {
            world.GameTime = world.GameTime.AddHours(8);
        }




        //public static DataWorld GetWorld()
        //{
        //    return SettlementGame.Web.GameContoller.world;

        //}

    }
}
