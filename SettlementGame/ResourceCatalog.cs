using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class ResourceCatalog
    {
        public static ResourceCategory GetCategory(ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.Meat: return ResourceCategory.Food;
                case ResourceType.Berries: return ResourceCategory.Food;
                case ResourceType.CleanWater: return ResourceCategory.Water;
                case ResourceType.Wood: return ResourceCategory.Material;
                case ResourceType.Stone: return ResourceCategory.Material;
                case ResourceType.Gold: return ResourceCategory.Material;
                case ResourceType.Doska: return ResourceCategory.Material;
                case ResourceType.Kirpich: return ResourceCategory.Material;
                case ResourceType.Moneta: return ResourceCategory.Money;
                default: throw new ArgumentOutOfRangeException();
               
            
            }
        }
    }
}
