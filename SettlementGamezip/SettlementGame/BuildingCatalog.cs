using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class BuildingCatalog
    {
        public ResourceType InputResource { get; }

        public int AmountOfInputResource { get; set; }
        public ResourceType OutputResource { get; set; }
        public int AmountOfOutputResource { get; set; }

        public BuildingCatalog (ResourceType InputResource, int AmountOfInputResource, ResourceType OutputResource, int AmountOfOutputResource)
        {
            this.InputResource = InputResource;
            this.AmountOfInputResource = AmountOfInputResource;
            this.OutputResource = OutputResource;
            this.AmountOfOutputResource = AmountOfOutputResource;
        }



        public static BuildingCatalog GetProductForCreation(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.WoodMakery:
                    return new BuildingCatalog(ResourceType.Wood,1, ResourceType.Doska,5); //найти в ресурсах мира дерево, потребить и создать доски
                    //или вернуть тип потребляемого ресурса и его кол-во, тип производимого ресурса и его кол-во.
                case BuildingType.StoneMakery:
                    return new BuildingCatalog(ResourceType.Stone, 1, ResourceType.Kirpich, 4);
                case BuildingType.GoldMakery:
                        return new BuildingCatalog(ResourceType.Gold, 1, ResourceType.Moneta, 10);

                default: throw new ArgumentOutOfRangeException();

            }
        }
    }
}
