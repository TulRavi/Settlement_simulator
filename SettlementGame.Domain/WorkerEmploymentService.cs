using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class WorkerEmploymentService
    {
        public void FireWorker(DataWorld world, Worker worker)
        {
            if (worker.WorkPlace == null)
                return;

            Building building = worker.WorkPlace;

            // 1. снять связи
            worker.UnassignWithWorkPlace();
            building.RemoveWorker();

            // 2. выдать компенсацию
            world.ResourceList.Find(a => a.ResourceType == ResourceType.Moneta).Increase(-3);

            // 3. лог мб впоследствии
        }
        public void AssignWorker(DataWorld world, Worker worker, Building building)
        {
            // если здание занято — увольняем текущего
            if (building.HasEmployee)
            {
                FireWorker(world, building.AssignedWorker);
            }

            worker.AssignWithWorkPlace(building);
            building.AssignWorker(worker);
        }
        public void ChangeWorkingHours(Worker worker,TimeSpan startWorkingTime, TimeSpan endWorkingTime)
        {
            worker.StartWorkingTime = startWorkingTime;
            worker.EndWorkingTime = endWorkingTime;
        }
        public void FindWorker(DataWorld world,int id,out int temp,out bool isFound)
        {
            temp = 0;
            isFound = false;
            for (int i = 0; i < world.WorkersList.Count; i++)
            {
                if (world.WorkersList.ElementAt(i).Id == id)
                {
                    temp = i;
                    isFound = true;
                    break;
                }
            }
        }
        public static bool DeleteWorker(DataWorld world,int id)
        {
            int temp = 0;
            bool isFound = false;
            world.workerEmploymentService.FindWorker(world, id, out temp, out isFound);
            if (isFound == true)
            {
                world.WorkersList.RemoveAt(temp);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void ClearWorkersList(DataWorld world)
        {
            world.WorkersList.Clear();
            Console.WriteLine($"Worker list was cleared");
             
        }
        //WorldService.GetWorkerDtoList(world)
    }
}
