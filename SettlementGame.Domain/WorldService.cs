using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Text;
using static SettlementGame.Domain.WorldService;


namespace SettlementGame.Domain
{   //CreateWorld

//GetWorld

//ResetWorld
    public class WorldService
    {

        private readonly DataWorld _world;
        private readonly WorldCreator _worldCreator;
        private readonly GameDbContext _dbContext;
        private readonly WorkerEmploymentService _workerEmploymentService;
        public BuildingType BuildingType { get; }


        public WorldService(DataWorld world, WorldCreator worldCreator, GameDbContext dbContext, WorkerEmploymentService workerEmploymentService)
        {
            this._world = world;
            this._worldCreator = worldCreator;
            this._dbContext = dbContext;
            this._workerEmploymentService = workerEmploymentService;
        }
        public DataWorld CreateWorld(int numberOfWorkers=3)
    {
            //DataWorld world=_worldCreator.CreateWorld();
            //DataWorld world = new DataWorld();

            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Meat, 100));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Berries, 202));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.CleanWater, 500));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Wood, 40));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Stone, 40));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Gold, 10));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Doska, 0));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Kirpich, 0));
            //world.SettlementResourceList.Add(new AnyResource(ResourceType.Moneta, 100));

            //_worldCreator.AddPossibleBuildings(world);

            //_worldCreator.CreateDateTime(world);

            //world.workerEmploymentService = WorldCreator.CreateWorkerEmploymentService(world);
            _worldCreator.CreateWorld();
        _worldCreator.CreateWorkers(numberOfWorkers, _world);
            _world.CurrentCrownTask=CrownTask.CreateNewCrownTack(_world,_dbContext);
            

        return _world;
    }

        public class WorkerDto
        {
            public int Id { get; set; }
            public int WorkPlaceId { get; set; }

            public bool IsAlive { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public double PersonalLoyality { get; set; }
            public int PersonalMoney { get; set; }
            //public TimeSpan StartWorkingTime { get; set; }
            //public TimeSpan EndWorkingTime { get; set; }

        }

        public List<WorkerDto> GetWorkerDtoList()
        {
            List<WorkerDto> workerDtoList = new List<WorkerDto>();
            foreach (WorkerEntity workerEntity in _dbContext.Workers)
            {
                Worker worker = WorkerMapper.ToDomain(workerEntity);
                WorkerDto workerDto = new WorkerDto();
                workerDto.Id = worker.Id;
                workerDto.WorkPlaceId= (int)worker.WorkPlaceId;
                workerDto.X = worker.X;
                workerDto.Y = worker.Y;
                workerDto.IsAlive = worker.IsAlive;
                workerDto.PersonalLoyality = worker.PersonalLoyality;
                workerDto.PersonalMoney = worker.PersonalMoney;
                workerDtoList.Add(workerDto);
            }
            return workerDtoList;
        }

        public class BuildingDto
        {
            public int? Id { get; set; }
            public BuildingType BuildingType { get; set; }
            public bool HasEmployee { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public int? AssignedWorkerId { get; set; }
        }

        public class CrownTaskDto
        {
            public string ResourceType { get; set; }
            public int Amount { get; set; }
            public int NumberOfTicks { get; set; }
            public double LoyalityCounter { get; set; }
        }

        public CrownTaskDto GetCurrentCrownTaskDto()
        {
            var task = _world.CurrentCrownTask;

            if (task == null)
                return null;

            return new CrownTaskDto
            {
                ResourceType = task.Resource.ResourceType.ToString(),
                Amount = task.Resource.Amount,
                NumberOfTicks = task.NumberOfTicks,
                LoyalityCounter = task.LoyalityCounter
            };
        }

        public List<SettlementGame.Domain.AnyResource> GetSettlementResourceList()
        {
            return _world.SettlementResourceList;
        }
        public List<BuildingDto> GetBuildingDtoList()
        {
            List<BuildingDto> BuildingDtoList = new List<BuildingDto>();

            //foreach (Building building in _dbContext.BuildedBuildings)
            //{
            //    BuildingDto buildingDto = new BuildingDto();
            //    buildingDto.Id = building.Id;
            //    buildingDto.X = building.X;
            //    buildingDto.Y = building.Y;
            //    buildingDto.HasEmployee = building.HasEmployee;
            //    buildingDto.BuildingType = building.BuildingType;
            //    BuildingDtoList.Add(buildingDto);
            //}
            foreach (BuildingEntity entity in _dbContext.BuildedBuildings)
            {
                Building building = BuildingMapper.ToDomain(entity);

                BuildingDto buildingDto = new BuildingDto
                {
                    Id = building.Id,
                    X = building.X,
                    Y = building.Y,
                    HasEmployee = building.HasEmployee,
                    BuildingType = building.BuildingType,
                    AssignedWorkerId=building.AssignedWorkerId
                };

                BuildingDtoList.Add(buildingDto);
            }
            return BuildingDtoList;
        }


        //public List<Building> GetBuildings()
        //{
        //    return world.BuildingList;
        //}

        public List<BuildingType> GetPossibleBuildings()
        {

            return Enum.GetValues(typeof(BuildingType))
              .Cast<BuildingType>()//приводим enum к изначальным значеням
              .ToList();
        }

        public CrownTask GetCurrentCrownTask()
        {
            return _world.CurrentCrownTask;
        }

        public bool CreateBuilding(BuildingType buildingType) {
            CreateBuildingContext createBuildingContext = new CreateBuildingContext(buildingType);
            bool exists = Enum.IsDefined(typeof(BuildingType), createBuildingContext.BuildingType);

            {
                if (exists == true)
                {
                    CreateBuildingAction action = (CreateBuildingAction)UsersActionsCatalog.CreateBuildingAction(createBuildingContext);
                    //context.Building = world.BuildingList.Find(x => x.Id == Id);
                    action.Execute(_world);

                    //будем тут добалвять в БД, чтобы избежать связки работы с конекртной БД в доменной части
                    var entity = BuildingMapper.ToEntity(createBuildingContext.CreatingBuilding);
                    _dbContext.BuildedBuildings.Add(entity);
                    _dbContext.SaveChanges();


                    return true;
                }
                else { return false; }
            }
        }

        public int FindBuilding(int Id)
        {
            var entity = _dbContext.BuildedBuildings.FirstOrDefault(x => x.Id == Id);
            if (entity == null) { return -1; } else { return (int)entity.Id; }
                
        }
        public bool RemoveBuilding(int Id)
        {   
            var entity = _dbContext.BuildedBuildings.FirstOrDefault(x => x.Id == Id);
            if (entity == null)
                return false;
            var building = BuildingMapper.ToDomain(entity);
            //Building building = _dbContext.BuildedBuildings.FirstOrDefault(x => x.Id== Id);
            //DestroyBuildingContext destroyBuildingContext = new DestroyBuildingContext(building, _workerEmploymentService);
            if (building.HasEmployee == true&&building.AssignedWorkerId!=null)
            {
                _workerEmploymentService.FireWorker(building.AssignedWorker.Id);
                //DestroyBuildingContext.Building.
                //Program.TempFireWorkerDirectly(world, DestroyBuildingContext.Building, DestroyBuildingContext.Building.AssignedWorker);
            }
            
            //DestroyBuildingAction action = (DestroyBuildingAction)UsersActionsCatalog.DestroyBuildingAction(destroyBuildingContext);
            //context.Building = world.BuildingList.Find(x => x.Id == Id);
            //action.Execute(world);
            //entity = BuildingMapper.ToEntity(destroyBuildingContext.DestroyingBuilding,WorkerEmploymentService);

            _dbContext.BuildedBuildings.Remove(entity);
            _dbContext.SaveChanges();
            return true;
        }

        public void PrintAllPossibleBuildings(DataWorld world)
        {
            foreach (BuildingType building in world.PossibleBuildingList)
            {
                Console.WriteLine($"{building.ToString()}");
            }
        }

        public double GetPeopleLoyality()
        {
            return _world.Peopleloyality;
        }


        public void CreateWorkersByService(int numberOfWorkers=3)
        {
            _worldCreator.CreateWorkers(numberOfWorkers, _world);
        }

        public void Tick() 
        {
            int temp = _world.GetHashCode();
            UpdateWorkers(_world);
            UpdateBuildings(_world);
            //UpdateResources();
            UpdateLoyalty(_world);
            UpdateCrownTask(_world, _dbContext);
            //updateGameState(_world);
        }

        

        private void UpdateWorkers(DataWorld world)
        {
            foreach (var workerEntity in _dbContext.Workers)
            {
                int temp = world.GetHashCode();
                Worker worker = WorkerMapper.ToDomain(workerEntity);
                worker.Tick(world);
                workerEntity.IsAlive = worker.IsAlive;
                workerEntity.X = worker.X;
                workerEntity.Y = worker.Y;
                workerEntity.IsEmployed = worker.IsEmployed;
                workerEntity.PersonalLoyality = worker.PersonalLoyality;

            }
            _dbContext.SaveChanges();
        }

        public void UpdateCrownTask(DataWorld world,GameDbContext dbContext)
        {
            if (CrownTask.IsCompleted(world) == true)
            {
                CrownTask task = world.CurrentCrownTask;
                AnyResource reqAnyResource = task.Resource;
                world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount = world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount - reqAnyResource.Amount;
                world.CurrentCrownTask=CrownTask.CreateNewCrownTack(world, dbContext);
            }
            else
            { 
                world.CurrentCrownTask.NumberOfTicks--;
                if (world.CurrentCrownTask.NumberOfTicks <= 0) 
                {
                    world.CrownLoyaity = world.CrownLoyaity - 0.1;
                    world.CurrentCrownTask = CrownTask.CreateNewCrownTack(world, dbContext);
                }
            }
            ;
        }

        public void UpdateBuildings(DataWorld world)
        {
            foreach (var entity in _dbContext.BuildedBuildings)
            {
                var building = BuildingMapper.ToDomain(entity);
                if (building.HasEmployee== true) { building.Tick(world); }
            }
        }
        public int updateGameState(DataWorld world) 
        {
            if (world.CrownLoyaity <= 0)
            {
                return world.GameState = -1;
            }
            if(world.CrownLoyaity >= 1) 
            {
                return world.GameState = 1;
            }
            return 0;
        }
               

        private void UpdateLoyalty(DataWorld world)
        { double tempLoyality = 0;
            foreach (var worker in _dbContext.Workers)
            {
                tempLoyality= tempLoyality+worker.PersonalLoyality;
            }
            world.Peopleloyality= tempLoyality / _dbContext.Workers.Count();
            
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
