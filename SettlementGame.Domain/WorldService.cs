using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Text;
using static SettlementGame.Domain.WorldService;


namespace SettlementGame.Domain
{   

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
        _worldCreator.CreateDefaultWorkers(numberOfWorkers, _world);
            _world.CurrentCrownTask=CreateNewCrownTack();
            

        return _world;
    }

        public class WorkerDTO
        {
            public int Id { get; set; }
            public int WorkPlaceId { get; set; }

            public bool IsAlive { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public double PersonalLoyality { get; set; }
            public int PersonalMoney { get; set; }
            public string info { get; set; }
            //public TimeSpan StartWorkingTime { get; set; }
            //public TimeSpan EndWorkingTime { get; set; }

        }

        public List<WorkerDTO> GetWorkerDtoList()
        {
            List<WorkerDTO> workerDtoList = new List<WorkerDTO>();
            foreach (WorkerEntity workerEntity in _dbContext.Workers)
            {
                Worker worker = WorkerMapper.ToDomain(workerEntity);
                WorkerDTO workerDto = new WorkerDTO();
                workerDto.Id = worker.Id;
                workerDto.WorkPlaceId= (int)worker.WorkPlaceId;
                workerDto.X = worker.X;
                workerDto.Y = worker.Y;
                workerDto.IsAlive = worker.IsAlive;
                workerDto.PersonalLoyality = worker.PersonalLoyality;
                workerDto.PersonalMoney = worker.PersonalMoney;
                workerDto.info = worker.info;
                workerDtoList.Add(workerDto);
            }
            return workerDtoList;
        }

        public class WorkerOrderDTO
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

        public class CrownTaskDTO
        {
            public string ResourceType { get; set; }
            public int Amount { get; set; }
            public int NumberOfTicks { get; set; }
            public double LoyalityCounter { get; set; }
        }

        public CrownTaskDTO GetCurrentCrownTaskDTO()
        {
            var task = _world.CurrentCrownTask;

            if (task == null)
                return null;

            return new CrownTaskDTO
            {
                ResourceType = task.Resource.ResourceType.ToString(),
                Amount = task.Resource.Amount,
                NumberOfTicks = task.NumberOfTicks,
                LoyalityCounter = task.LoyalityCounter
            };
        }

        public class SettlementResourceDTO
        {
            public ResourceType ResourceType { get; set; }
            public int Amount { get; set; }
        }

        public List<SettlementResourceDTO> GetSettlementResourceListDTO()
        {
            List<SettlementResourceDTO> settlementResourceListDTO = new List<SettlementResourceDTO>();

            foreach (Resource resource in _world.SettlementResourceList) {
                SettlementResourceDTO settlementResourceDTO = new SettlementResourceDTO
                {
                    ResourceType = resource.ResourceType,
                    Amount = resource.Amount
                };
                settlementResourceListDTO.Add(settlementResourceDTO);
            }
            return settlementResourceListDTO;
        }

        public class PeopleLoayalityDTO
        {
            public double PeopleLoyality { get; set; }
        }

        public PeopleLoayalityDTO GetPeopleLoyalityDTO()
        {
            return new PeopleLoayalityDTO
            {
                PeopleLoyality = _world.Peopleloyality
            };
        }

        public class CrownLoayalityDTO
        {
            public double CrownLoyality { get; set; }
        }

        public CrownLoayalityDTO GetCrownLoyalityDTO()
        {
            return new CrownLoayalityDTO
            {
                CrownLoyality = _world.CrownLoyaity
            };
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
                    AssignedWorkerId=building.AssignedWorkerId,
                    info =building.info
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
                bool isPossibleToBeFired=_workerEmploymentService.FireWorker((int)building.AssignedWorkerId);
                if (isPossibleToBeFired == true)
                {
                    _dbContext.BuildedBuildings.Remove(entity);
                    _dbContext.SaveChanges();
                    return true;
                }
                else return false;
                //DestroyBuildingContext.Building.
                //Program.TempFireWorkerDirectly(world, DestroyBuildingContext.Building, DestroyBuildingContext.Building.AssignedWorker);
            }
            else return false;

            //DestroyBuildingAction action = (DestroyBuildingAction)UsersActionsCatalog.DestroyBuildingAction(destroyBuildingContext);
            //context.Building = world.BuildingList.Find(x => x.Id == Id);
            //action.Execute(world);
            //entity = BuildingMapper.ToEntity(destroyBuildingContext.DestroyingBuilding,WorkerEmploymentService);


        }

        public void PrintAllPossibleBuildings(DataWorld world)
        {
            foreach (BuildingType building in world.PossibleBuildingList)
            {
                Console.WriteLine($"{building.ToString()}");
            }
        }

        

        public void CreateWorkersByService(int numberOfWorkers=3)
        {
            _worldCreator.CreateDefaultWorkers(numberOfWorkers, _world);
        }

        public void Tick() 
        {
            int temp = _world.GetHashCode();
            UpdateWorkers();
            UpdateBuildings();
            //UpdateResources();
            UpdateWorkersLoyalty();
            //UpdateCrownLoyality();
            UpdateCrownTask();
            _workerEmploymentService.UpdateWorkerOrders(_world);
            //updateGameState(_world);
        }

        

        private void UpdateWorkers()
        {
            foreach (var workerEntity in _dbContext.Workers)
            {
                int temp = _world.GetHashCode();
                Worker worker = WorkerMapper.ToDomain(workerEntity);
                List<Need> temp2 = worker.workerNeeds;
                worker.Tick(_world.standartSalary,_world.SettlementResourceList);

                //добавим блок для запоминания со+стояния потребностей тупо в цифрах, чтобы не переусложнять БД
                workerEntity.Hunger =worker.GetNeed<NeedHunger>().Amount;
                workerEntity.Thirst =worker.GetNeed<NeedThirst>().Amount;
                workerEntity.Alcohol =worker.GetNeed<NeedAlcohol>().Amount;
                workerEntity.Salary= worker.GetNeed<NeedSalary>().Amount;
                workerEntity.IsAlive = worker.IsAlive;
                workerEntity.X = worker.X;
                workerEntity.Y = worker.Y;
                workerEntity.IsEmployed = worker.IsEmployed;
                workerEntity.PersonalLoyality = worker.PersonalLoyality;
                workerEntity.info = worker.info;
                workerEntity.PersonalMoney = worker.PersonalMoney;
                //тут сделаем проверку, что чел не умер
                if (workerEntity.IsAlive == false)
                {
                    _dbContext.Workers.Remove(workerEntity);
                    _world.Peopleloyality -= -0.1;
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
                _world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount = _world.SettlementResourceList.Find(x => x.ResourceType == reqAnyResource.ResourceType).Amount - reqAnyResource.Amount;
                _world.CrownLoyaity += task.LoyalityCounter;
                _world.CurrentCrownTask=CreateNewCrownTack();
            }
            else
            { 
                _world.CurrentCrownTask.NumberOfTicks--;
                if (_world.CurrentCrownTask.NumberOfTicks <= 0) 
                {
                    _world.CrownLoyaity = _world.CrownLoyaity - 0.1;
                    _world.CurrentCrownTask = CreateNewCrownTack();
                }
            }
            ;
        }

        public CrownTask CreateNewCrownTack()
        {
            
            Random random = new Random();
            int numberOfResourses = Enum.GetValues(typeof(ResourceType)).Length;
            int selecteResourceNumber = random.Next(numberOfResourses);
            //int selecteResourceNumber = world.tempTpCheck;
            ResourceType requestedResourceType = (ResourceType)selecteResourceNumber;
            int numberOfWorkers = _dbContext.Workers.Count();
            //BuildingType buildingType=world.PossibleBuildingList.Find(x => x.GetType == Id)
            int nubmerOfBuildings = Enum.GetValues(typeof(BuildingType)).Length;
            bool isPossibleTask = false;
            CrownTask tempCrownTask = new CrownTask();

            //исключаем таверну и пр.здания не произв.ресурсы
            //var buildingTypes = Enum.GetValues(typeof(BuildingType)).Cast<BuildingType>().Where(b =>BuildingCatalog.GetProductForCreation(b).Outputs.Count > 0).ToList();

            for (int x = 0; x < nubmerOfBuildings; x++)//перебирем здания
            {
                BuildingType buildingType = (BuildingType)x;//поочередно
                //поочередно
                BuildingCatalog tempBuildingCatalog = BuildingCatalog.GetProductForCreation(buildingType);
                //получаем каталог производимых ресурсов и ищем, есть ли в нем нужный нам
                if (tempBuildingCatalog.Outputs.FirstOrDefault(x => x.ResourceType == requestedResourceType) != null)
                {
                    //если нашли, считаем сколько можно произвести с текущими рабочими
                    int productionOfOneBuilding = tempBuildingCatalog.Outputs.FirstOrDefault(x => x.ResourceType == requestedResourceType).Amount;
                    int productionOfAllPossibleBuildings = productionOfOneBuilding * numberOfWorkers;
                    int amountCounter = random.Next(3, 9);
                    //вводим дабл для деления, ибо при делении инт на инт резтат будет инт.
                    double loyalityCounter = amountCounter / _world.denominator;
                    //int reqAmount = random.Next(productionOfAllPossibleBuildings*amountCounter);
                    int reqAmount = productionOfAllPossibleBuildings * amountCounter;
                    //чтобы не было уберпросто, было логино и подталкивало к развию, мы просим произвести больше, чем есть
                    if (_world.SettlementResourceList.Find(x => x.ResourceType == requestedResourceType).Amount > reqAmount)
                    {
                        reqAmount = _world.SettlementResourceList.Find(x => x.ResourceType == requestedResourceType).Amount + reqAmount;
                        loyalityCounter = loyalityCounter + 0.05;
                        //подумать над логикой, мб это лишает мотивации создавать запас. корректируем лоялитиКаунтером
                    }
                    Resource resourse = new Resource(requestedResourceType, reqAmount);
                    int numberOfTicks = reqAmount / productionOfAllPossibleBuildings + 5;
                    isPossibleTask = true;
                    tempCrownTask = new CrownTask(resourse, numberOfTicks, loyalityCounter);
                    //world.tempTpCheck++;
                    break;
                }
            }
            if (isPossibleTask == false)
            {
                throw new ArgumentException("Невозможно создать задaние");
            }
            return tempCrownTask;
            //BuildingType buildingType=new BuildingType();

        }

        public void UpdateBuildings()
        {
            foreach (var entity in _dbContext.BuildedBuildings)
            {
                var building = BuildingMapper.ToDomain(entity);
                if (building.HasEmployee== true) { building.Tick(_world); }
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
               

        private void UpdateWorkersLoyalty()
        { double tempLoyality = 0;
            foreach (var worker in _dbContext.Workers)
            {
                tempLoyality= tempLoyality+worker.PersonalLoyality;
            }
            _world.Peopleloyality= tempLoyality / _dbContext.Workers.Count();
            if (_world.Peopleloyality <= 0) { _world.denominator = 15;}
            if (_world.Peopleloyality >= 1) { _world.denominator = 45; }

        }

        
        public static bool ChangeResourseAmount(DataWorld world, Resource resource)
        {
            Resource selectedResource=world.SettlementResourceList.Find(x => x.ResourceType == resource.ResourceType);
            if ((selectedResource.Amount - resource.Amount) > 0)
            {
                selectedResource.Amount -= resource.Amount;
                return true;
            }
            else return false;
            ;
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
