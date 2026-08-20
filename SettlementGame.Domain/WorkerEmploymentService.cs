using System;
using System.Collections.Generic;
using System.Linq;

namespace SettlementGame.Domain
{
    public class WorkerEmploymentService
    {
        private readonly GameDbContext _dbContext;
        private readonly DataWorld _world;

        public WorkerEmploymentService(GameDbContext dbContext, DataWorld world)
        {
            this._dbContext = dbContext;
            this._world = world;
        }

        public List<WorkerEntity> GetAllWorkers()
        {
            return _dbContext.Workers.ToList(); //convert the database table to a List
        }

        public WorkerEntity GetWorkerById(int id)
        {
            return _dbContext.Workers.FirstOrDefault(x => x.Id == id); //example: dbContext.Workers.FirstOrDefault(x => x.Id == 1)
            //this is translated into an SQL query similar to: SELECT * FROM Workers WHERE Id = id;
        }

        public bool FireWorker(int id)
        { WorkerEntity tempWorkerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            if (tempWorkerEntity.WorkPlaceId == -1 || tempWorkerEntity.WorkPlaceId == null)
            {
                return false;
            }

            WorkerEntity workerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            int? buildingId = workerEntity.WorkPlaceId;
            BuildingEntity buildingEntity = _dbContext.BuiltBuildings.FirstOrDefault(x => x.Id == buildingId);

            var worker = WorkerMapper.ToDomain(workerEntity);
            var building = BuildingMapper.ToDomain(buildingEntity);

            if (worker != null && worker.IsEmployed == true)
            {
                FireWorkerContext fireWorkerContext = new FireWorkerContext(worker, building);

                FireWorkerAction action =
                    (FireWorkerAction)UsersActionsCatalog.FireWorkerAction(fireWorkerContext);

                //Firing a worker costs three standard salaries.
                int compensation = _world.standartSalary * 3;

                Resource resource = new Resource(ResourceType.Coin, compensation);
                bool isCompleted = WorldService.ChangeResourceAmount(_world, resource);

                if (isCompleted == true)
                {
                    action.Execute(_world);

                    workerEntity.WorkPlaceId = fireWorkerContext.Worker.WorkPlaceId;
                    workerEntity.IsEmployed = fireWorkerContext.Worker.IsEmployed;

                    buildingEntity.AssignedWorkerId = fireWorkerContext.Building.AssignedWorkerId;

                    _dbContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CreateOrderForNewWorkers(int number, DataWorld world)
        {
            //Check if the settlement has enough money to hire the specified number of workers.
            int amountOfCoin =
                world.SettlementResourceList.Find(x => x.ResourceType == ResourceType.Coin).Amount;

            int reqMoney = number * 30;

            if (reqMoney <= amountOfCoin)
            {
                world.SettlementResourceList
                    .Find(x => x.ResourceType == ResourceType.Coin)
                    .Amount -= reqMoney;

                world.WorkerOrders.Add(
                    new DataWorld.WorkerOrder
                    {
                        Amount = number,
                        TicksLeft = world.TicksToRoad
                    });

                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateWorkerOrders(DataWorld world)
        {
            foreach (var order in world.WorkerOrders)
            {
                order.TicksLeft--;
            }

            var completedOrders =
                world.WorkerOrders
                     .Where(x => x.TicksLeft <= 0)
                     .ToList();

            foreach (var order in completedOrders)
            {
                CreateWorkers(order.Amount);
                world.WorkerOrders.Remove(order);
            }
        }

        public void CreateWorkers(int numberOfWorkers)
        {
            for (int i = 0; i < numberOfWorkers; i++)
            {
                //Worker needs are added to the worker's needs list.
                List<Need> workerNeeds = new List<Need>();
                workerNeeds.Add(new NeedHunger());
                workerNeeds.Add(new NeedThirst());
                workerNeeds.Add(new NeedAlcohol());
                workerNeeds.Add(new NeedSalary());

                Worker worker = new Worker(workerNeeds);

                worker.IsAlive = true;
                worker.WorkPlaceId = -1;
                worker.X = 0;
                worker.Y = 0;
                worker.PersonalLoyalty = 0.5;
                worker.PersonalMoney = 30;

                _dbContext.Workers.Add(WorkerMapper.ToEntity(worker));

                worker.StartWorkingTime = new TimeSpan(00, 00, 01);
                worker.EndWorkingTime = new TimeSpan(00, 00, 01);
            }

            _dbContext.SaveChanges();
        }

        public bool HireWorker(int id, int Id)
        {
            var workerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            var buildingEntity = _dbContext.BuiltBuildings.FirstOrDefault(x => x.Id == Id);

            if (workerEntity == null || buildingEntity == null)
                return false;

            var worker = WorkerMapper.ToDomain(workerEntity);
            var building = BuildingMapper.ToDomain(buildingEntity);

            bool isBuildingWorkerPossibleToBeFired = false;

            if (worker.WorkPlaceId == building.AssignedWorkerId & worker.WorkPlaceId != -1)
            {
                return false;
            }

            if (worker.WorkPlaceId != -1 && worker.WorkPlaceId != null)
            {
                isBuildingWorkerPossibleToBeFired = FireWorker(worker.Id);

                if (isBuildingWorkerPossibleToBeFired == false)
                {
                    return false;
                }
            }

            if (isBuildingWorkerPossibleToBeFired == true &&
                building.AssignedWorkerId != -1 &&
                building.AssignedWorkerId != null)
            {
                bool isNewBuildingWorkerPossibleToBeFired =
                    FireWorker((int)building.AssignedWorkerId);

                if (isNewBuildingWorkerPossibleToBeFired == false)
                {
                    return false;
                }
            }

            var hireWorkerContext = new HireWorkerContext(worker, building);

            var action =
                (HireWorkerAction)UsersActionsCatalog.HireWorkerAction(hireWorkerContext);

            action.Execute(_world);

            workerEntity.IsEmployed = hireWorkerContext.Worker.IsEmployed;
            workerEntity.WorkPlaceId = hireWorkerContext.Worker.WorkPlace?.Id;

            buildingEntity.AssignedWorkerId = hireWorkerContext.Worker.Id;

            _dbContext.SaveChanges();

            return true;
        }

        public void ChangeWorkingHours(
            Worker worker,
            TimeSpan startWorkingTime,
            TimeSpan endWorkingTime)
        {
            worker.StartWorkingTime = startWorkingTime;
            worker.EndWorkingTime = endWorkingTime;
        }

        public bool FindWorker(int id)
        {
            bool result = false;

            WorkerEntity workerEntity =
                _dbContext.Workers.FirstOrDefault(x => x.Id == id);

            if (workerEntity != null)
            {
                return result = true;
            }
            else
            {
                return false;
            }
        }

        public Worker GetWorkerByID(int id)
        {
            Worker worker =
                WorkerMapper.ToDomain(_dbContext.Workers.FirstOrDefault(x => x.Id == id));

            return worker;
        }

        public bool DeleteWorker(int id)
        {
            WorkerEntity worker =
                _dbContext.Workers.FirstOrDefault(x => x.Id == id);

            if (worker == null)
                return false;

            _dbContext.Workers.Remove(worker);
            _dbContext.SaveChanges();

            return true;
        }

        public void ClearWorkersList()
        {
            _dbContext.Workers.RemoveRange();
            _dbContext.SaveChanges();

            Console.WriteLine($"Worker list was cleared");
        }

        public void ChangeWorkersLifeState(int id, bool isAlive)
        {
            _dbContext.Workers.FirstOrDefault(x => x.Id == id).IsAlive = isAlive;
            _dbContext.SaveChanges();
        }

        public void ChangeWorker(
            int id,
            int X,
            int Y,
            bool isAlive,
            bool IsEmployed,
            double PersonalLoyalty)
        {
            WorkerEntity worker =
                _dbContext.Workers.FirstOrDefault(x => x.Id == id);

            if (worker == null)
                return;

            worker.X = X;
            worker.Y = Y;
            worker.IsAlive = isAlive;
            worker.IsEmployed = IsEmployed;
            worker.PersonalLoyalty = PersonalLoyalty;

            _dbContext.SaveChanges();
        }

        public List<DataWorld.WorkerOrder> GetTicksTillNewWorkers()
        {
            return _world.WorkerOrders;
        }
    }
}