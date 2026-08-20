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

        public int? AssignedWorkerId { get; set; }

        public string info => $"id:{Id} {BuildingType} workerId{AssignedWorkerId}";


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

        // -1 means that the building does not have an assigned worker.
        public bool HasEmployee => AssignedWorkerId != -1 && AssignedWorkerId != null;




        public bool IsBuild { get; private set; }
        public bool IsOpenedForUser { get; private set; }

        public Building(BuildingType BuildingType)
        {
            this.BuildingType = BuildingType;
        }



        public void Tick(DataWorld world)//the building should send a request to Create a resource here
        {
            BuildingCatalog buildingCatalog = BuildingCatalog.GetProductForCreation(BuildingType);//get an instance of the building catalog class

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

                for (int x = 0; x < buildingCatalog.Outputs.Count(); x++)//add the produced resource to the settlement resources
                {
                    foreach (Resource resource1 in world.SettlementResourceList)
                        if (resource1.ResourceType == buildingCatalog.Outputs.ElementAt(x).ResourceType)
                        {
                            resource1.Increase(buildingCatalog.Outputs.ElementAt(x).Amount);
                            continue;
                        }

                }
                for (int y = 0; y < buildingCatalog.Inputs.Count(); y++)//change the amount of materials in the settlement by removing the consumed resources
                {
                    foreach (Resource resource2 in world.SettlementResourceList)
                        if (resource2.ResourceType == buildingCatalog.Inputs.ElementAt(y).ResourceType)
                        {
                            resource2.Decrease(buildingCatalog.Inputs.ElementAt(y).Amount);
                            continue;
                        }
                }
            }
        }


    }
}