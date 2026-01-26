using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class Building
    {   
        public BuildingType BuildingType { get; }

        public Building(BuildingType BuildingType)
        {
            this.BuildingType = BuildingType;
        }

        public void CreateSomething(DataWorld world)//тут здание должно отправлять запрос на создание ресурсу
        {   
            BuildingCatalog buildingCatalog = BuildingCatalog.GetProductForCreation(BuildingType);//забираем экземпляр класса каталог строительства
            foreach(ResourceOfSettlement resource in world.ResourceList)
            {
                if(resource.ResourceType== buildingCatalog.InputResource) {
                    if (resource.TryToConsume(buildingCatalog.AmountOfInputResource) == true) {
                        foreach (ResourceOfSettlement resource1 in world.ResourceList)
                            if (resource1.ResourceType == buildingCatalog.OutputResource)
                            {
                                resource1.Increase(buildingCatalog.AmountOfOutputResource);
                            }
                            }
                }
            }
            
        }
        

    }
}
