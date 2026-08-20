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
        public BuildingCatalog(
        List<ResourceAmount> inputs,
        List<ResourceAmount> outputs)
        {
            Inputs = inputs;
            Outputs = outputs;
        }


        //Returns the production recipe for the specified building type.
        public static BuildingCatalog GetProductForCreation(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.Sawmill:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Wood, 1) },
                    new List<ResourceAmount> { new ResourceAmount(ResourceType.Plank, 5) }); //find wood in the world resources, consume it and Create planks
                                                                                             //or return the type and amount of the consumed resource and the type and amount of the produced resource.
                case BuildingType.Brickworks:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Stone, 1) },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Brick, 4) });
                case BuildingType.GoldMine:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Gold, 3) });

                case BuildingType.Mint:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Gold, 1) },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Coin, 10) });
                case BuildingType.Brewery:
                    return new BuildingCatalog(new List<ResourceAmount>
                        {
                        new ResourceAmount(ResourceType.WaterWell, 10),
                        new ResourceAmount(ResourceType.Wheat, 1),
                        new ResourceAmount(ResourceType.Hops, 1)
                        },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Beer, 10) });
                //case BuildingType.Tavern:
                //    return new BuildingCatalog(new List<ResourceAmount>(), new List<ResourceAmount>());//worker needs are handled elsewhere, not here
                case BuildingType.Hunter:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Meat, 5) });
                case BuildingType.BerryGathery:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Berries, 5) });
                case BuildingType.WaterWell:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.WaterWell, 5) });
                case BuildingType.LoggingCamp:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Wood, 5) });
                case BuildingType.StoneQuarry:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Stone, 5) });
                case BuildingType.Winery:
                    return new BuildingCatalog(new List<ResourceAmount> { new ResourceAmount(ResourceType.Berries, 10) },
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Wine, 3) });
                case BuildingType.WheatFarm:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Wheat, 5) });
                case BuildingType.HopFarm:
                    return new BuildingCatalog(new List<ResourceAmount>(),
                        new List<ResourceAmount> { new ResourceAmount(ResourceType.Hops, 5) });


                default: throw new ArgumentOutOfRangeException();

            }
        }
    }
}