using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SettlementGame.Domain.WorldService;

namespace SettlementGame.Domain
{
    public class WorkerEmploymentService
    {


        //новая верися с ббазой данных
        private readonly GameDbContext _dbContext;

        public WorkerEmploymentService(GameDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<WorkerEntity> GetAllWorkers()
        {
            return _dbContext.Workers.ToList(); // из бд-таблицы лист преобразуем
        }

        public WorkerEntity GetWorkerById(int id)
        {
            return _dbContext.Workers.FirstOrDefault(x => x.Id == id); //пример: dbContext.Workers.FirstOrDefault(x => x.Id == 1)
            //это выполняет sql SELECT * FROM Workers WHERE Id = id; 
        }
        //пока закомменирую старое
        private readonly DataWorld world;

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
        {
            //int buildingId = _dbContext.Workers.FirstOrDefault(x => x.Id == id).WorkPlace.BuildingId;
            WorkerEntity workerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            var worker = WorkerMapper.ToDomain(workerEntity);
            if (worker != null && worker.IsEmployed == true)
            {

                FireWorkerContext fireWorkerContext = new FireWorkerContext(worker);

                FireWorkerAction action = (FireWorkerAction)UsersActionsCatalog.FireWorkerAction(fireWorkerContext);
                //context.Building = world.BuildingList.Find(x => x.BuildingId == buildingId);
                action.Execute(world);

                workerEntity = WorkerMapper.ToEntity(fireWorkerContext.Worker);
                _dbContext.Workers.Remove(workerEntity);
                _dbContext.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool HireWorker(int id, int BuildingId)
        {
            var workerEntity = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            var buildingEntity = _dbContext.BuildedBuildings.FirstOrDefault(x => x.BuildingId == BuildingId);

            if (workerEntity == null || buildingEntity == null)
                return false;

            //var worker = workerEntity; // пока без WorkerMapper
            var worker = WorkerMapper.ToDomain(workerEntity);//добавили маппер
            var building = BuildingMapper.ToDomain(buildingEntity);

            if (building.HasEmployee)
            {
                FireWorker(building.AssignedWorker.Id);
            }

            var hireWorkerContext = new HireWorkerContext(worker, building);

            var action = (HireWorkerAction)UsersActionsCatalog.HireWorkerAction(hireWorkerContext);

            action.Execute(world);
            //var tempWorkerEntity = WorkerMapper.ToEntity(hireWorkerContext.Worker);
            //var tempWorker = WorkerMapper.ToDomain(tempWorkerEntity);
            workerEntity.IsEmployed = hireWorkerContext.Worker.IsEmployed;
            workerEntity.WorkPlaceId = hireWorkerContext.Worker.WorkPlace?.BuildingId;
            _dbContext.SaveChanges();
            return true;
            //bool isFound;

            //    Worker worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
            //    Building building = _dbContext.BuildedBuildings.FirstOrDefault(x => x.BuildingId == BuildingId);
            //if (worker != null && building != null)
            //{
            //    if (building.HasEmployee)
            //    {
            //        FireWorker(building.AssignedWorker.Id);
            //    }
            //    HireWorkerContext hireWorkerContext = new HireWorkerContext(worker, building);

            //    FireWorkerAction action = (FireWorkerAction)UsersActionsCatalog.HireWorkerAction(hireWorkerContext);
            //    //context.Building = world.BuildingList.Find(x => x.BuildingId == buildingId);
            //    action.Execute(world);
            //    return true;
            //}
            //else { return false; }

        }

        //    Worker worker = _dbContext.Workers.FirstOrDefault(x => x.Id == id);
        //    if (worker != null)
        //    {
        //        Building building=world.BuildingList.Find(x => x.BuildingId == BuildingId);
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
        //WorldService.GetWorkerDtoList(world)
    }
}
