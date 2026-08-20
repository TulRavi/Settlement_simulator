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
        private readonly GameDbContext _dbContext; //new private field naming convention, remember this
        private readonly DataWorld _world;

        public WorldCreator(
            GameDbContext dbContext,
            DataWorld world)
        {
            _dbContext = dbContext;
            _world = world;
        }

        public DataWorld CreateWorld()
        {
            //Creating a new world also resets the current database state.
            ResetDatabase();

            _world.SettlementResourceList.Clear();

            //Get all values defined in the ResourceType enum.
            foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
            {
                Resource resource = new Resource(resourceType, 0);
                _world.SettlementResourceList.Add(resource);
            }

            AddOrUpdateItem(ResourceType.Meat, 100);
            AddOrUpdateItem(ResourceType.Berries, 202);
            AddOrUpdateItem(ResourceType.WaterWell, 500);
            AddOrUpdateItem(ResourceType.Wood, 40);
            AddOrUpdateItem(ResourceType.Stone, 40);
            AddOrUpdateItem(ResourceType.Gold, 10);
            AddOrUpdateItem(ResourceType.Coin, 500);
            AddOrUpdateItem(ResourceType.Wheat, 10);
            AddOrUpdateItem(ResourceType.Hops, 10);

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
            var existingItem = _world.SettlementResourceList
                .Find(x => x.ResourceType == resourceType);

            if (existingItem != null)
            {
                existingItem.Amount += amount; //update the amount
            }
            else
            {
                _world.SettlementResourceList.Add(
                    new Resource(resourceType, amount));
            }
        }

        public void ResetDatabase()
        {
            //If the database schema is broken or missing a field, the database file may need to be deleted manually.
            _dbContext.Workers.RemoveRange(_dbContext.Workers);
            _dbContext.BuiltBuildings.RemoveRange(_dbContext.BuiltBuildings);

            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSqlRaw(
                "DELETE FROM sqlite_sequence WHERE name='Workers';"); //sqlite_sequence has a state of autoincrement inside - need to reset every new cycle;

            _dbContext.Database.ExecuteSqlRaw(
                "DELETE FROM sqlite_sequence WHERE name='BuiltBuildings';");
        }

        public void CreateDateTime(DataWorld world)
        {
            DateTime gameTime = new DateTime(0001, 01, 31, 08, 00, 0);
            world.GameTime = gameTime;
        }

        public void AddPossibleBuildings(DataWorld world)
        {
            //Get all values defined in the BuildingType enum.
            Array buildingTypes = Enum.GetValues(typeof(BuildingType));

            foreach (BuildingType buildingType in buildingTypes)
            {
                world.PossibleBuildingList.Add(buildingType);
            }
        }

        public void CreateDefaultWorkers(int numberOfWorkers, DataWorld world)
        {
            for (int i = 0; i < numberOfWorkers; i++)
            {
                //Worker needs are added to the needs list.
                List<Need> workerNeeds = new List<Need>();
                workerNeeds.Add(new NeedHunger());
                workerNeeds.Add(new NeedThirst());
                workerNeeds.Add(new NeedAlcohol());
                workerNeeds.Add(new NeedSalary());

                Worker worker = new Worker(workerNeeds); //Create a worker with the specified needs

                worker.IsAlive = true;
                worker.WorkPlaceId = -1;
                worker.CurrentSalary = world.standartSalary;

                worker.X = 0;
                worker.Y = 0;
                worker.PersonalLoyalty = 0.5;
                worker.PersonalMoney = 30;

                _dbContext.Workers.Add(WorkerMapper.ToEntity(worker)); //add the worker to the database instead of the world list

                worker.StartWorkingTime = new TimeSpan(00, 00, 01);
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
                    Console.WriteLine(
                        $"worker {i} {need.ToString()} {need.Amount} workingTimeFrom:{worker.StartWorkingTime} to:{worker.EndWorkingTime}");
                }

                i++;
            }

            i = 0;

            foreach (Resource resource in world.SettlementResourceList)
            {
                Console.WriteLine(
                    $"{resource} {resource.ResourceType.ToString()} {resource.Amount}");
            }
        }

        public void PrintBuiltBuildings(DataWorld world)
        {
            foreach (BuildingEntity buildingEntity in _dbContext.BuiltBuildings)
            {
                Console.WriteLine(
                    $"{buildingEntity.GetType().ToString()} index of building:{buildingEntity.Id} has Employee {buildingEntity.AssignedWorkerId}");
            }
        }

        //public void PrintWorkersList()
        //{
        //    foreach (WorkerEntity worker in _dbContext.Workers)
        //    {
        //        Console.WriteLine(
        //            $"index of a worker:{_dbContext.Workers.Find(worker)} if employeed {worker.IsEmployed}");
        //    }
        //}

        public void PrintHiredWorkersList(DataWorld world)
        {
            foreach (WorkerEntity workerEntity in _dbContext.Workers)
            {
                if (workerEntity.IsEmployed == true)
                {
                    Console.WriteLine(
                        $"index of a worker:{_dbContext.Workers.Find(workerEntity)} is working in {workerEntity.WorkPlaceId.ToString()}");
                }
            }
        }
    }
}