using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class WorkerEmploymentService
    {

        private readonly DataWorld world;

        public WorkerEmploymentService(DataWorld world)
        {
            this.world = world;
        }

        public bool GetWorkerById(int id)
        {
            int temp = 0;
            bool isFound = false;
            FindWorker(id, out isFound);

            if (isFound == true) { return true; } else { return false; }
        }
        public bool FireWorker(int id)
        {
            //int buildingId = world.WorkersList.Find(x => x.Id == id).WorkPlace.BuildingId;
            Worker worker = world.WorkersList.Find(x => x.Id == id);
            if (worker != null && worker.IsEmployed == true)
            {

                FireWorkerContext fireWorkerContext = new FireWorkerContext(worker);

                FireWorkerAction action = (FireWorkerAction)UsersActionsCatalog.FireWorkerAction(fireWorkerContext);
                //context.Building = world.BuildingList.Find(x => x.BuildingId == buildingId);
                action.Execute(world);
                return true;
            }
            else return false;
        }

        public bool HireWorker(int id, int BuildingId)
        { bool isFound;
            
                Worker worker = world.WorkersList.Find(x => x.Id == id);
                Building building = world.BuildingList.Find(x => x.BuildingId == BuildingId);
            if (worker != null && building != null)
            {
                if (building.HasEmployee)
                {
                    FireWorker(building.AssignedWorker.Id);
                }
                HireWorkerContext hireWorkerContext = new HireWorkerContext(worker, building);

                FireWorkerAction action = (FireWorkerAction)UsersActionsCatalog.HireWorkerAction(hireWorkerContext);
                //context.Building = world.BuildingList.Find(x => x.BuildingId == buildingId);
                action.Execute(world);
                return true;
            }
            else { return false; }
            
        }

        //    Worker worker = world.WorkersList.Find(x => x.Id == id);
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
        public void FindWorker(int id,out bool isFound)
        {
            
            isFound = false;
            for (int i = 0; i < world.WorkersList.Count; i++)
            {
                if (world.WorkersList.ElementAt(i).Id == id)
                {
                    
                    isFound = true;
                    break;
                }
            }
        }
        public bool DeleteWorker(int id)
        {
            //int temp = 0;
            Worker worker = world.WorkersList.Find(x => x.Id == id);
            if (worker == null)
                return false;

            world.WorkersList.Remove(worker);
            return true;
        }
        public void ClearWorkersList()
        {
            world.WorkersList.Clear();
            Console.WriteLine($"Worker list was cleared");
             
        }

        public void ChangeWorkersLifeState(int id, bool isAlive) 
        {
            world.WorkersList.Find(x=>x.Id==id).IsAlive = isAlive;
        }

        
        public void ChangeWorker(int id, int X,int Y, bool isAlive)
        {
            Worker worker = world.WorkersList.Find(x => x.Id == id);

            if (worker == null)
                return;

            worker.X = X;
            worker.Y = Y;
            worker.IsAlive = isAlive;
        }
        //WorldService.GetWorkerDtoList(world)
    }
}
