using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class BuildingEntity
    {
        public int BuildingId { get; set; }

        public BuildingType BuildingType { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int? AssignedWorkerId { get; set; }
    }
}
