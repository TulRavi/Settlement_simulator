using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class WorldCreator
    {
        private readonly GameDbContext _dbContext;//новый нэйминг , запомнить
        private readonly DataWorld _world;

        public WorldCreator(
            GameDbContext dbContext,
            DataWorld world)
        {
            _dbContext = dbContext;
            _world = world;
        }
        //public WorldCreator(GameDbContext dbContext)
        //{
        //    _dbContext = dbContext; //получили контекст базы данных чз консструктор
        //}
        public DataWorld CreateWorld()
            

        {   //List <Resource> listResource=new List <Resource>();

            //DataWorld world = new DataWorld();
            //в-1 работает нестабильно
            //_dbContext.Database.EnsureDeleted();
            //_dbContext.Database.EnsureCreated();//видимо,нужно закрывать соединение для пересоздвания

            //в-2 не работает 
            // Удаляем все записи из таблицы (перечислить все нужные таблицы)
            //string tableName = "Workers"; // Имя таблицы
            //_dbContext.Database.ExecuteSqlRaw("DELETE FROM {0}", tableName);
            //_dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE {0}", tableName);
            //string tableName2 = "BuildedBuildings"; // Имя таблицы
            //_dbContext.Database.ExecuteSqlRaw("DELETE FROM {0}", tableName2);
            //_dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE {0}", tableName2);

            //в-3 не работает
            //var entityTypes = _dbContext.Model.GetEntityTypes();

            //foreach (var entityType in entityTypes)
            //{
            //    var tableName = entityType.GetTableName();
            //    if (tableName != null)
            //    {
            //        // Выполняем SQL-запрос на удаление всех строк для каждой таблицы
            //        _dbContext.Database.ExecuteSqlRawAsync($"DELETE FROM {tableName}");
            //    }
            //}
            //в-4
            ResetDatabase();
            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                Resource resource = new Resource(resourceType, 0);
                // Здесь можно выполнить проверку или изменить значение
                _world.SettlementResourceList.Add(resource);
            }

            AddOrUpdateItem(ResourceType.Meat, 100);
            AddOrUpdateItem(ResourceType.Berries, 202);
            AddOrUpdateItem(ResourceType.CleanWater, 500);
            AddOrUpdateItem(ResourceType.Wood, 40);
            AddOrUpdateItem(ResourceType.Stone, 40);
            AddOrUpdateItem(ResourceType.Gold, 10);
            AddOrUpdateItem(ResourceType.Moneta, 100);
            AddOrUpdateItem(ResourceType.Psheniza, 10);
            AddOrUpdateItem(ResourceType.Hmel, 10);
            
            

            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Meat, 100));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Berries, 202));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.CleanWater, 500));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Wood, 40));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Stone, 40));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Gold, 10));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Doska, 0));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Kirpich, 0));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Moneta, 100));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Psheniza, 10));
            //_world.SettlementResourceList.Add(new AnyResource(ResourceType.Hmel, 10));
            AddPossibleBuildings(_world);
            CreateDateTime(_world);
            _world.GameState = 0;
            _world.CrownLoyaity = 0.5;
            _world.denominator = 30;
            _world.standartSalary = 10;
            return _world;
        }

        private void AddOrUpdateItem(ResourceType resourceType, int amount)
        {
            var existingItem = _world.SettlementResourceList.Find(x => x.ResourceType == resourceType);

            if (existingItem != null)
            {
                existingItem.Amount += amount; // Обновляем количество
            }
            else
            {
                _world.SettlementResourceList.Add(new Resource(resourceType, amount));
            }
        }

        public void ResetDatabase()
        {
            _dbContext.Workers.RemoveRange(_dbContext.Workers); // при ошибке типа нет поля ХХ как костыль вручную удаляем файл
            _dbContext.BuildedBuildings.RemoveRange(_dbContext.BuildedBuildings);
            
            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSqlRaw(
                "DELETE FROM sqlite_sequence WHERE name='Workers';");

            _dbContext.Database.ExecuteSqlRaw(
                "DELETE FROM sqlite_sequence WHERE name='BuildedBuildings';");
        }

        //public static WorkerEmploymentService CreateWorkerEmploymentService(DataWorld world)
        //{
        //    WorkerEmploymentService workerEmploymentService = new WorkerEmploymentService(world);
        //    return workerEmploymentService;
        //}

        public void CreateDateTime(DataWorld world)
        {
            DateTime gameTime = new DateTime(0001, 01, 31, 08, 00, 0);
            world.GameTime = gameTime;
        }

        public void AddPossibleBuildings(DataWorld world)
        {
            Array buildingTypes = Enum.GetValues(typeof(BuildingType));

            foreach (BuildingType buildingType in buildingTypes)
            {
                world.PossibleBuildingList.Add(buildingType);
            }
        }

        
        public void CreateDefaultWorkers(int numberOfWorkers, DataWorld world)
        {   
            for (int i = 0; i < numberOfWorkers; i++)
            { //Потребности добавлены в лист потребностей
                List<Need> workerNeeds = new List<Need>();
                workerNeeds.Add(new NeedHunger());
                workerNeeds.Add(new NeedThirst());
                workerNeeds.Add(new NeedAlcohol());
                workerNeeds.Add(new NeedSalary());
                Worker worker = new Worker(workerNeeds);//создан рабочий с заданными потребностями
                worker.IsAlive = true;
                worker.WorkPlaceId = -1;
                
                //world.WorkersList.Add(worker);//рабочий с заданнами потербностями добавлен в лист рабочих
                
                //worker.Id = world.NextWorkerId;
                worker.X = 0;
                worker.Y = 0;
                //worker.WorkPlace = null;
                //worker.IsEmployed = false;
                worker.PersonalLoyality = 0.5;
                worker.PersonalMoney = 30;
                _dbContext.Workers.Add(WorkerMapper.ToEntity(worker)); //вместо листа доабвляем в БД

                //world.NextWorkerId++;
                worker.StartWorkingTime = new TimeSpan(00,00,01);
                worker.EndWorkingTime = new TimeSpan(00, 00, 01);
            }
            _dbContext.SaveChanges();
        }
        public void CreateCrownTask()
        {
           
        }
        public void PrintState(DataWorld world)
        {
            int i = 0;
            Console.WriteLine($"{world.GameTime}");
            foreach (WorkerEntity workerEntity in _dbContext.Workers)
            {
                Worker worker = WorkerMapper.ToDomain(workerEntity);
                foreach (Need need in worker.workerNeeds)
                {
                    Console.WriteLine($"worker {i} {need.ToString()} {need.Amount} workingTimeFrom:{worker.StartWorkingTime} to:{worker.EndWorkingTime}");
                }
                i++;
            }
            i = 0;
            foreach (Resource resource in world.SettlementResourceList)
            {
             Console.WriteLine($"{resource} {resource.ResourceType.ToString()} {resource.Amount}");
            }
        }

        public void PrintBuildedBuildings(DataWorld world)
        {
            foreach (BuildingEntity buildingEntity in _dbContext.BuildedBuildings)
            {
                Console.WriteLine($"{buildingEntity.GetType().ToString()} index of building:{buildingEntity.Id} has Employee {buildingEntity.AssignedWorkerId}");
            }
        }

        public void PrintWorkersList()
        {
            foreach (WorkerEntity worker in _dbContext.Workers)
            {
                Console.WriteLine($"index of a worker:{_dbContext.Workers.Find(worker)} if employeed {worker.IsEmployed}");
            }
        }

        public void PrintHiredWorkersList(DataWorld world)
        {
            foreach (WorkerEntity workerEntity in _dbContext.Workers)
            {
                if (workerEntity.IsEmployed == true)
                {
                    Console.WriteLine($"index of a worker:{_dbContext.Workers.Find(workerEntity)} is working in {workerEntity.WorkPlaceId.ToString()}");
                }
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

        //public static bool TryAssignWorker(DataWorld world, Worker worker, Building building)
        //{
        //    if (worker.IsEmployed)
        //        return false;

        //    if (building.HasEmployee)
        //        return false;

        //    worker.AssignWithWorkPlace(building);
        //    building.AssignWorker(worker);

        //    return true;
        //}

        //public static void UnassignWorker(Worker worker)
        //{
        //    if (worker.WorkPlace == null)
        //        return;

        //    Building building = worker.WorkPlace;

        //    worker.UnassignWithWorkPlace();
        //    building.RemoveWorker();
        //}


    }
}
