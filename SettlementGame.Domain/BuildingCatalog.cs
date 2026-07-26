using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class BuildingCatalog
    {
        public List<ResourceAmount> Inputs { get; set; }
        public List<ResourceAmount> Outputs { get; set; }
        //public ResourceType InputResource { get; }

        //public int AmountOfInputResource { get; set; }
        //public ResourceType OutputResource { get; set; }
        //public int AmountOfOutputResource { get; set; }

        //public BuildingCatalog (ResourceType InputResource, int AmountOfInputResource, ResourceType OutputResource, int AmountOfOutputResource)
        //{
        //    this.InputResource = InputResource;
        //    this.AmountOfInputResource = AmountOfInputResource;
        //    this.OutputResource = OutputResource;
        //    this.AmountOfOutputResource = AmountOfOutputResource;
        //}
        public BuildingCatalog(
        List<ResourceAmount> inputs,
        List<ResourceAmount> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
        }


        public static BuildingCatalog GetProductForCreation(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.DoskaMakery:
                    return new BuildingCatalog(new List<ResourceAmount>{new ResourceAmount(ResourceType.Wood,1) },
                    new List<ResourceAmount> { new ResourceAmount(ResourceType.Doska, 5) }); //найти в ресурсах мира дерево, потребить и создать доски
                    //или вернуть тип потребляемого ресурса и его кол-во, тип производимого ресурса и его кол-во.
                case BuildingType.KirpichMakery:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Stone, 1) },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Kirpich, 4) });
                case BuildingType.GoldMakery:
                        return new BuildingCatalog(new List<ResourceAmount>(),
                            new List<ResourceAmount> { new ResourceAmount(ResourceType.Gold, 3) });

                case BuildingType.MonetaMakery:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Gold, 1) },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Moneta, 10) });
                case BuildingType.BeerMakery:
                    return new BuildingCatalog(new List<ResourceAmount>
                        {
                        new ResourceAmount(ResourceType.CleanWater, 10),
                        new ResourceAmount(ResourceType.Psheniza, 1),
                        new ResourceAmount(ResourceType.Hmel, 1)
                        },
                        new List<ResourceAmount>{new ResourceAmount(ResourceType.Beer, 10)});
                //case BuildingType.Tavern:
                //    return new BuildingCatalog(new List<ResourceAmount>(), new List<ResourceAmount>());//в удовлетворении нужды рабочего потребление, не тут
                case BuildingType.MeatMakery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Meat, 5) });
                case BuildingType.BerriesMakery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Berries, 5) });
                case BuildingType.CleanWater:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.CleanWater, 5) });
                case BuildingType.WoodMakery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Wood, 5) });
                case BuildingType.StoneMakery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Stone, 5) });
                case BuildingType.WineMakery:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Berries, 10) },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Wine, 3) });
                case BuildingType.PshenizaMakery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Psheniza, 5) });
                case BuildingType.HmelMakery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Hmel, 5) });
                

                default: throw new ArgumentOutOfRangeException();

            }
        }
    }
}
