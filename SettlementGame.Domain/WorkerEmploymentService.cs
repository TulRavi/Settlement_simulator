using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.OpenApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SettlementGame.Domain.WorldService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SettlementGame.Domain
{
    public class WorkerEmploymentService
    {


        //новая верися с ббазой данных
        private readonly GameDbContext _dbContext;
        private readonly DataWorld _world;

        public WorkerEmploymentService(GameDbContext dbContext,DataWorld world)
        {
            this._dbContext = dbContext;
            this._world = world;
        }

        public List<WorkerEntity> GetAllWorkers()
        {
            return _dbContext.Workers.ToList(); // из бд-таблицы в лист преобразуем
        }

        public WorkerEntity GetWorkerById(int id)
        {
            return _dbContext.Workers.FirstOrDefault(x => x.Id == id); //пример: dbContext.Workers.FirstOrDefault(x => x.Id == 1)
            //это выполняет sql SELECT * FROM Workers WHERE Id = id; 
        }
        //пока закомменирую старое
        

        //public WorkerEmploymentService(DataWorld world)
        //{
        //    this.world = world;
        //}

        //public bool GetWorkerById(int id)
        //{
        //    int temp = 0;
        //    bool isFound = false;
        //    FindWorker(id, out isFound);

        //    if (isFound == true) { return true; } else { return false; }
        //}
        public bool FireWorker(int id)
        {   if(_dbContext.Workers.FirstOrDefault(x => x.Id == id).WorkPlaceId==-1|| _dbContext.Workers.FirstOrDefault(x => x.Id == id).WorkPlaceId == null) { return false; }
            
            WorkerEntity workerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            int?buildingId = workerEntity.WorkPlaceId;
            BuildingEntity buildingEntity = _dbContext.BuildedBuildings.FirstOrDefault(x => x.Id == buildingId);
            var worker = WorkerMapper.ToDomain(workerEntity);
            var building= BuildingMapper.ToDomain(buildingEntity);
            if (worker != null && worker.IsEmployed == true)
            {

                FireWorkerContext fireWorkerContext = new FireWorkerContext(worker,building);

                FireWorkerAction action = (FireWorkerAction)UsersActionsCatalog.FireWorkerAction(fireWorkerContext);
                //context.Building = world.BuildingList.Find(x => x.Id == Id);
                int compensation = _world.standartSalary * 3;
                Resource resource = new Resource(ResourceType.Moneta, compensation);
                bool isCompleted = WorldService.ChangeResourseAmount(_world, resource);
                if (isCompleted == true)
                {
                    action.Execute(_world);
                    workerEntity.WorkPlaceId = fireWorkerContext.Worker.WorkPlaceId;

                    workerEntity.IsEmployed = fireWorkerContext.Worker.IsEmployed;

                    buildingEntity.AssignedWorkerId = fireWorkerContext.Building.AssignedWorkerId;

                    _dbContext.SaveChanges();

                    return true;
                } else return false;
                //workerEntity = WorkerMapper.ToEntity(fireWorkerContext.Worker);
                //_dbContext.Workers.Update(workerEntity);
                //buildingEntity = BuildingMapper.ToEntity(fireWorkerContext.Building);
                //_dbContext.BuildedBuildings.Update(buildingEntity);

            }
            else return false;
        }

        public bool CreateOrderForNewWorkers(int number, DataWorld world)
        //проверяем, что есть деньги на найм(равен бюджету работника)
        {
            int amountOfMoneta = world.SettlementResourceList.Find(x => x.ResourceType == ResourceType.Moneta).Amount;
            int reqMoney = number * 30;
            if (reqMoney <= amountOfMoneta)
            { 
                world.SettlementResourceList.Find(x => x.ResourceType == ResourceType.Moneta).Amount -= reqMoney;
                world.WorkerOrders.Add(new DataWorld.WorkerOrder { Amount = number, TicksLeft = world.TicksToRoad });
                return true;
            }
            else { return false; }
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
            { //Потребности добавлены в лист потребностей
                List<Need> workerNeeds = new List<Need>();
                workerNeeds.Add(new NeedHunger());
                workerNeeds.Add(new NeedThirst());
                workerNeeds.Add(new NeedAlcohol());
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
                worker.StartWorkingTime = new TimeSpan(00, 00, 01);
                worker.EndWorkingTime = new TimeSpan(00, 00, 01);
            }
            _dbContext.SaveChanges();
        }

        public bool HireWorker(int id, int Id)
        {
            var workerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            var buildingEntity = _dbContext.BuildedBuildings.FirstOrDefault(x => x.Id == Id);

            if (workerEntity == null || buildingEntity == null)
                return false;

            //var worker = workerEntity; // пока без WorkerMapper
            var worker = WorkerMapper.ToDomain(workerEntity);//добавили маппер
            var building = BuildingMapper.ToDomain(buildingEntity);
            bool isBuildingWorkerPossibleToBeFired=false;
            if (worker.WorkPlaceId == building.AssignedWorkerId) { return false; }
            if (worker.WorkPlaceId!=-1&&worker.WorkPlaceId!=null)
            {
                isBuildingWorkerPossibleToBeFired=FireWorker(worker.Id);
                if(isBuildingWorkerPossibleToBeFired == false) { return false; }
            }
            if (isBuildingWorkerPossibleToBeFired==true&&building.AssignedWorkerId != -1&& building.AssignedWorkerId !=null)
            {
                bool isNewBuildingWorkerPossibleToBeFired = FireWorker((int)building.AssignedWorkerId);
                if (isNewBuildingWorkerPossibleToBeFired == false) { return false; }
            }


            var hireWorkerContext = new HireWorkerContext(worker, building);

            var action = (HireWorkerAction)UsersActionsCatalog.HireWorkerAction(hireWorkerContext);

            action.Execute(_world);
            //var tempWorkerEntity = WorkerMapper.ToEntity(hireWorkerContext.Worker);
            //var tempWorker = WorkerMapper.ToDomain(tempWorkerEntity);
            workerEntity.IsEmployed = hireWorkerContext.Worker.IsEmployed;
            workerEntity.WorkPlaceId = hireWorkerContext.Worker.WorkPlace?.Id;
            
            buildingEntity.AssignedWorkerId = hireWorkerContext.Worker.Id;
            _dbContext.SaveChanges();
            return true;
            //bool isFound;

            //    Worker worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            //    Building building = _dbContext.BuildedBuildings.FirstOrDefault(x => x.Id == Id);
            //if (worker != null && building != null)
            //{
            //    if (building.HasEmployee)
            //    {
            //        FireWorker(building.AssignedWorker.Id);
            //    }
            //    HireWorkerContext hireWorkerContext = new HireWorkerContext(worker, building);

            //    FireWorkerAction action = (FireWorkerAction)UsersActionsCatalog.HireWorkerAction(hireWorkerContext);
            //    //context.Building = world.BuildingList.Find(x => x.Id == Id);
            //    action.Execute(world);
            //    return true;
            //}
            //else { return false; }

        }

        //    Worker worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
        //    if (worker != null)
        //    {
        //        Building building=world.BuildingList.Find(x => x.Id == Id);
        //        if (building == null)
        //            return false;
        //        // если здание занято — увольняем текущего
        //        

        //        worker.AssignWithWorkPlace(building);
        //        building.AssignWorker(worker);
        //        return true;
        //    }
        //    else return false;
        //}
        public void ChangeWorkingHours(Worker worker,TimeSpan startWorkingTime, TimeSpan endWorkingTime)
        {
            worker.StartWorkingTime = startWorkingTime;
            worker.EndWorkingTime = endWorkingTime;
        }
        public bool FindWorker(int id)
        {
            bool result = false;
            WorkerEntity workerEntity= _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            if (workerEntity != null)
            {
                return result = true;
            }
            else return false;
        }

        public Worker GetWorkerByID(int id)
        {
            Worker worker = WorkerMapper.ToDomain(_dbContext.Workers.FirstOrDefault(x => x.Id == id));
            return worker;
        }
        public bool DeleteWorker(int id)
        {
            //int temp = 0;
            WorkerEntity worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
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
            _dbContext.Workers.FirstOrDefault(x=>x.Id==id).IsAlive = isAlive;
            _dbContext.SaveChanges();
        }


        public void ChangeWorker(int id, int X, int Y, bool isAlive, bool IsEmployed,double PersonalLoyality)
        {
            WorkerEntity worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);

            if (worker == null)
                return;

            worker.X = X;
            worker.Y = Y;
            worker.IsAlive = isAlive;
            worker.IsEmployed = IsEmployed;
            
            worker.PersonalLoyality = PersonalLoyality;


       
        _dbContext.SaveChanges();
        }
        public List<DataWorld.WorkerOrder> GetTicksTillNewWorkers()
        {
            return _world.WorkerOrders;
        }
        //WorldService.GetWorkerDtoList(world)
    }
}
