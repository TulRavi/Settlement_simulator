using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SettlementGame.Domain
{
    public class Building
    {

        public BuildingType BuildingType { get; set; }
        //public bool HasEmployee { get; private set; }
        public int? Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Worker AssignedWorker { get; private set; }

        public int?AssignedWorkerId { get; set; }

        internal void AssignWorker(Worker worker)
        {
            AssignedWorker = worker;
            AssignedWorkerId = worker.Id;
        }

        internal void RemoveWorker()
        {
            AssignedWorker = null;
            AssignedWorkerId = -1;
        }

        public bool HasEmployee => AssignedWorkerId != -1 && AssignedWorkerId != null;

        //private bool hasEmployee;

        //public bool HasEmployee
        //{
        //    get { return hasEmployee; }
        //    set { hasEmployee = value;
        //        if (hasEmployee == false)
        //        {
        //            //AssignedWorker != null;
        //        }
        //    }
        //}



        public bool IsBuild { get; private set; }
        public bool IsOpenedForUser { get; private set; }

        public Building(BuildingType BuildingType)
        {
            this.BuildingType = BuildingType;
        }



        public void Tick(DataWorld world)//тут здание должно отправлять запрос на создание ресурсу
        {
            BuildingCatalog buildingCatalog = BuildingCatalog.GetProductForCreation(BuildingType);//забираем экземпляр класса каталог строительства

            bool canProduce = true;

            foreach (var input in buildingCatalog.Inputs)
            {
                var resource = world.SettlementResourceList
                    .Find(r => r.ResourceType == input.ResourceType);

                if (resource == null || resource.Amount < input.Amount)
                {
                    canProduce = false;
                    break;
                }
            }
            if (canProduce == true)
            {

                for (int x = 0; x < buildingCatalog.Outputs.Count(); x++)//добалвяем произведнный ресурс в ресурсы поседения
                {
                    foreach (AnyResource resource1 in world.SettlementResourceList)
                        if (resource1.ResourceType == buildingCatalog.Outputs.ElementAt(x).ResourceType)
                        {   
                            resource1.Increase(buildingCatalog.Outputs.ElementAt(x).Amount);
                            continue;
                        }
                    
                }
                for (int y = 0; y < buildingCatalog.Inputs.Count(); y++)//меняем кол-во материалов в поселении, убирая затраченные 
                {
                    foreach (AnyResource resource2 in world.SettlementResourceList)
                        if (resource2.ResourceType == buildingCatalog.Inputs.ElementAt(y).ResourceType)
                        {
                            resource2.Decrease(buildingCatalog.Inputs.ElementAt(y).Amount);
                            continue;
                        }
                }
            }
        }


        //public void CreateSomethingold(DataWorld world)//тут здание должно отправлять запрос на создание ресурсу
        //{
        //    BuildingCatalog buildingCatalog = BuildingCatalog.GetProductForCreation(BuildingType);//забираем экземпляр класса каталог строительства
        //    foreach (ResourceOfSettlement resource in world.ResourceList)
        //    {
        //        if (resource.ResourceType == buildingCatalog.InputResource)
        //        {
        //            if (resource.TryToConsume(buildingCatalog.AmountOfInputResource) == true)
        //            {
        //                foreach (ResourceOfSettlement resource1 in world.ResourceList)
        //                    if (resource1.ResourceType == buildingCatalog.OutputResource)
        //                    {
        //                        resource1.Increase(buildingCatalog.AmountOfOutputResource);
        //                    }
        //            }
        //        }
        //    }

        //}

    }
}
    



