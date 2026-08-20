using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public class ResourceCatalog
    {
        public static ResourceCategory GetCategory(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Meat: return ResourceCategory.Food;
                case ResourceType.Berries: return ResourceCategory.Food;
                case ResourceType.WaterWell: return ResourceCategory.Water;
                case ResourceType.Wood: return ResourceCategory.Material;
                case ResourceType.Stone: return ResourceCategory.Material;
                case ResourceType.Gold: return ResourceCategory.Material;
                case ResourceType.Plank: return ResourceCategory.Material;
                case ResourceType.Brick: return ResourceCategory.Material;
                case ResourceType.Coin: return ResourceCategory.Money;
                case ResourceType.Beer: return ResourceCategory.Alcohol;
                case ResourceType.Wine: return ResourceCategory.Alcohol;
                case ResourceType.Wheat: return ResourceCategory.Material;
                case ResourceType.Hops: return ResourceCategory.Material;
                default: throw new ArgumentOutOfRangeException();
               
            
            }
        }
    }
}
