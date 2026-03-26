using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class ResourceAmount
    {
        public ResourceType ResourceType { get; }
        public int Amount { get; }

        public ResourceAmount(ResourceType resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
        
    }
}
