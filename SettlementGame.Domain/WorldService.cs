using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SettlementGame.Domain
{
    public class WorldService
    {
        private readonly DataWorld _world;
        private readonly WorldCreator _worldCreator;
        private readonly GameDbContext _dbContext;
        private readonly WorkerEmploymentService _workerEmploymentService;

        public WorldService(
            DataWorld world,
            WorldCreator worldCreator,
            GameDbContext dbContext,
            WorkerEmploymentService workerEmploymentService)
        {
            _world = world;
            _worldCreator = worldCreator;
            _dbContext = dbContext;
            _workerEmploymentService = workerEmploymentService;
        }

        public DataWorld CreateWorld(int numberOfWorkers = 3)
        {
            _worldCreator.CreateWorld();
            _worldCreator.CreateDefaultWorkers(numberOfWorkers, _world);

            _world.CurrentCrownTask = CreateNewCrownTask();

            return _world;
        }

        public class WorkerDto
        {
            public int Id { get; set; }
            public int WorkPlaceId { get; set; }
            public bool IsAlive { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public double PersonalLoyalty { get; set; }
            public int PersonalMoney { get; set; }
            public string info { get; set; }
        }

        public List<WorkerDto> GetWorkerDtoList()
        {
            List<WorkerDto> workerDtoList = new List<WorkerDto>();

            foreach (WorkerEntity workerEntity in _dbContext.Workers)
            {
                Worker worker = WorkerMapper.ToDomain(workerEntity);

                WorkerDto workerDto = new WorkerDto
                {
                    Id = worker.Id,
                    WorkPlaceId = (int)worker.WorkPlaceId,
                    X = worker.X,
                    Y = worker.Y,
                    IsAlive = worker.IsAlive,
                    PersonalLoyalty = worker.PersonalLoyalty,
                    PersonalMoney = worker.PersonalMoney,
                    info = worker.info
                };

                workerDtoList.Add(workerDto);
            }

            return workerDtoList;
        }

        public class WorkerOrderDto
        {
            public int Amount { get; set; }
            public int TicksLeft { get; set; }
        }

        public class BuildingDto
        {
            public int? Id { get; set; }
            public BuildingType BuildingType { get; set; }
            public bool HasEmployee { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int? AssignedWorkerId { get; set; }
            public string info { get; set; }
        }

        public class CrownTaskDto
        {
            public string ResourceType { get; set; }
            public int Amount { get; set; }
            public int NumberOfTicks { get; set; }
            public double LoyaltyCounter { get; set; }
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
                LoyaltyCounter = task.LoyaltyCounter
            };
        }

        public class SettlementResourceDto
        {
            public ResourceType ResourceType { get; set; }
            public int Amount { get; set; }
        }

        public List<SettlementResourceDto> GetSettlementResourceListDto()
        {
            List<SettlementResourceDto> settlementResourceListDto =
                new List<SettlementResourceDto>();

            foreach (Resource resource in _world.SettlementResourceList)
            {
                SettlementResourceDto settlementResourceDto = new SettlementResourceDto
                {
                    ResourceType = resource.ResourceType,
                    Amount = resource.Amount
                };

                settlementResourceListDto.Add(settlementResourceDto);
            }

            return settlementResourceListDto;
        }

        public class PeopleLoayalityDto
        {
            public double PeopleLoyalty { get; set; }
        }

        public PeopleLoayalityDto GetPeopleLoyaltyDto()
        {
            return new PeopleLoayalityDto
            {
                PeopleLoyalty = _world.PeopleLoyalty
            };
        }

        public class CrownLoayalityDto
        {
            public double CrownLoyalty { get; set; }
        }

        public CrownLoayalityDto GetCrownLoyaltyDto()
        {
            return new CrownLoayalityDto
            {
                CrownLoyalty = _world.CrownLoyaity
            };
        }

        public List<BuildingDto> GetBuildingDtoList()
        {
            List<BuildingDto> buildingDtoList = new List<BuildingDto>();

            foreach (BuildingEntity entity in _dbContext.BuiltBuildings)
            {
                Building building = BuildingMapper.ToDomain(entity);

                BuildingDto buildingDto = new BuildingDto
                {
                    Id = building.Id,
                    X = building.X,
                    Y = building.Y,
                    HasEmployee = building.HasEmployee,
                    BuildingType = building.BuildingType,
                    AssignedWorkerId = building.AssignedWorkerId,
                    info = building.info
                };

                buildingDtoList.Add(buildingDto);
            }

            return buildingDtoList;
        }

        public List<BuildingType> GetPossibleBuildings()
        {
            //Convert all enum values to BuildingType values and then to a List.
            return Enum.GetValues(typeof(BuildingType))
                .Cast<BuildingType>()
                .ToList();
        }

        public bool CreateBuilding(BuildingType buildingType)
        {
            CreateBuildingContext CreateBuildingContext =
                new CreateBuildingContext(buildingType);

            bool exists = Enum.IsDefined(
                typeof(BuildingType),
                CreateBuildingContext.BuildingType);

            if (exists == true)
            {
                CreateBuildingAction action =
                    (CreateBuildingAction)UsersActionsCatalog
                        .CreateBuildingAction(CreateBuildingContext);

                action.Execute(_world);

                //The domain part Creates the building, while the WorldService is responsible for saving it to the database.
                var entity = BuildingMapper.ToEntity(
                    CreateBuildingContext.CreatingBuilding);

                _dbContext.BuiltBuildings.Add(entity);
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public int FindBuilding(int Id)
        {
            var entity = _dbContext.BuiltBuildings
                .FirstOrDefault(x => x.Id == Id);

            if (entity == null)
                return -1;

            return (int)entity.Id;
        }

        public bool RemoveBuilding(int Id)
        {
            var entity = _dbContext.BuiltBuildings
                .FirstOrDefault(x => x.Id == Id);

            if (entity == null)
                return false;

            var building = BuildingMapper.ToDomain(entity);

            if (building.HasEmployee == true &&
                building.AssignedWorkerId != null)
            {
                bool isPossibleToBeFired =
                    _workerEmploymentService.FireWorker(
                        (int)building.AssignedWorkerId);

                if (isPossibleToBeFired == false)
                    return false;
            }

            _dbContext.BuiltBuildings.Remove(entity);
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

        public void CreateWorkersByService(int numberOfWorkers = 3)
        {
            _worldCreator.CreateDefaultWorkers(numberOfWorkers, _world);
        }

        public void Tick()
        {
            //The order of updates is important because later systems depend on the state produced by earlier ones.
            UpdateWorkers();
            UpdateBuildings();
            UpdateWorkersLoyalty();
            UpdateCrownTask();
            _workerEmploymentService.UpdateWorkerOrders(_world);
        }

        private void UpdateWorkers()
        {
            foreach (var workerEntity in _dbContext.Workers)
            {
                Worker worker = WorkerMapper.ToDomain(workerEntity);

                worker.Tick(
                    _world.standartSalary,
                    _world.SettlementResourceList);

                //Store the current needs as simple numeric values instead of complicating the database model with Need entities.
                workerEntity.Hunger =
                    worker.GetNeed<NeedHunger>().Amount;

                workerEntity.Thirst =
                    worker.GetNeed<NeedThirst>().Amount;

                workerEntity.Alcohol =
                    worker.GetNeed<NeedAlcohol>().Amount;

                workerEntity.Salary =
                    worker.GetNeed<NeedSalary>().Amount;

                workerEntity.IsAlive = worker.IsAlive;
                workerEntity.X = worker.X;
                workerEntity.Y = worker.Y;
                workerEntity.IsEmployed = worker.IsEmployed;
                workerEntity.PersonalLoyalty = worker.PersonalLoyalty;
                workerEntity.info = worker.info;
                workerEntity.PersonalMoney = worker.PersonalMoney;

                //Check whether the worker died during the Tick.
                if (workerEntity.IsAlive == false)
                {
                    _dbContext.Workers.Remove(workerEntity);
                    _world.PeopleLoyalty -= 0.1;
                }
            }

            _dbContext.SaveChanges();
        }

        public void UpdateCrownTask()
        {
            if (_world.CurrentCrownTask.IsCompleted(_world) == true)
            {
                CrownTask task = _world.CurrentCrownTask;
                Resource reqAnyResource = task.Resource;

                _world.SettlementResourceList
                    .Find(x => x.ResourceType == reqAnyResource.ResourceType)
                    .Amount -= reqAnyResource.Amount;

                _world.CrownLoyaity += task.LoyaltyCounter;
                _world.CurrentCrownTask = CreateNewCrownTask();
            }
            else
            {
                _world.CurrentCrownTask.NumberOfTicks--;

                if (_world.CurrentCrownTask.NumberOfTicks <= 0)
                {
                    _world.CrownLoyaity -= 0.1;
                    _world.CurrentCrownTask = CreateNewCrownTask();
                }
            }
        }

        public CrownTask CreateNewCrownTask()
        {
            Random random = new Random();

            int numberOfResources =
                Enum.GetValues(typeof(ResourceType)).Length;

            int selecteResourceNumber =
                random.Next(numberOfResources);

            ResourceType requestedResourceType =
                (ResourceType)selecteResourceNumber;

            int numberOfWorkers = _dbContext.Workers.Count();

            int nubmerOfBuildings =
                Enum.GetValues(typeof(BuildingType)).Length;

            bool isPossibleTask = false;
            CrownTask tempCrownTask = new CrownTask();

            //Try each building to find one that can produce the requested resource.
            for (int x = 0; x < nubmerOfBuildings; x++)
            {
                BuildingType buildingType = (BuildingType)x;

                BuildingCatalog tempBuildingCatalog =
                    BuildingCatalog.GetProductForCreation(buildingType);

                //Check whether the building can produce the requested resource.
                if (tempBuildingCatalog.Outputs.FirstOrDefault(
                    x => x.ResourceType == requestedResourceType) != null)
                {
                    //Calculate how much can be produced by one building and by all currently available workers.
                    int productionOfOneBuilding =
                        tempBuildingCatalog.Outputs
                            .FirstOrDefault(
                                x => x.ResourceType == requestedResourceType)
                            .Amount;

                    int productionOfAllPossibleBuildings =
                        productionOfOneBuilding * numberOfWorkers;

                    int amountCounter = random.Next(3, 9);

                    //A double is used here because integer division would truncate the result.
                    double LoyaltyCounter =
                        amountCounter / _world.denominator;

                    int reqAmount =
                        productionOfAllPossibleBuildings * amountCounter;

                    //To make the task less predictable and encourage the player to develop production and maintain a stockpile, the requested amount can exceed the current production capacity.
                    if (_world.SettlementResourceList
                        .Find(x => x.ResourceType == requestedResourceType)
                        .Amount > reqAmount)
                    {
                        reqAmount =
                            _world.SettlementResourceList
                                .Find(x => x.ResourceType == requestedResourceType)
                                .Amount + reqAmount;

                        LoyaltyCounter += 0.05;

                        //Think about this logic later: it may discourage the player from maintaining a resource reserve.
                    }

                    Resource resourse =
                        new Resource(requestedResourceType, reqAmount);

                    int numberOfTicks =
                        reqAmount / productionOfAllPossibleBuildings + 5;

                    isPossibleTask = true;

                    tempCrownTask =
                        new CrownTask(
                            resourse,
                            numberOfTicks,
                            LoyaltyCounter);

                    break;
                }
            }

            if (isPossibleTask == false)
            {
                throw new ArgumentException("Unable to Create a crown task.");
            }

            return tempCrownTask;
        }

        public void UpdateBuildings()
        {
            foreach (var entity in _dbContext.BuiltBuildings)
            {
                var building = BuildingMapper.ToDomain(entity);

                if (building.HasEmployee == true)
                {
                    building.Tick(_world);
                }
            }
        }

        public int updateGameState(DataWorld world)
        {
            if (world.CrownLoyaity <= 0)
            {
                return world.GameState = -1;
            }

            if (world.CrownLoyaity >= 1)
            {
                return world.GameState = 1;
            }

            return 0;
        }

        private void UpdateWorkersLoyalty()
        {
            double tempLoyalty = 0;

            foreach (var worker in _dbContext.Workers)
            {
                tempLoyalty += worker.PersonalLoyalty;
            }
            if (_dbContext.Workers.Count() != 0)
            {
                _world.PeopleLoyalty = tempLoyalty / _dbContext.Workers.Count();
            }
            else _world.PeopleLoyalty = 0;

            if (_world.PeopleLoyalty <= 0)
            {
                _world.denominator = 15;
            }

            if (_world.PeopleLoyalty >= 1)
            {
                _world.denominator = 45;
            }
        }

        public static bool ChangeResourceAmount(
            DataWorld world,
            Resource resource)
        {
            Resource selectedResource =
                world.SettlementResourceList
                    .Find(x => x.ResourceType == resource.ResourceType);

            if ((selectedResource.Amount - resource.Amount) >= 0)
            {
                selectedResource.Amount -= resource.Amount;
                return true;
            }

            return false;
        }

        public void UpdateGameTime(DataWorld world)
        {
            world.GameTime = world.GameTime.AddHours(8);
        }
    }
}