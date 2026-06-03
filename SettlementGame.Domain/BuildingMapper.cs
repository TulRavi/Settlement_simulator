using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public static class BuildingMapper
    {
        public static Building ToDomain(BuildingEntity entity)
        {
            var building = new Building(entity.BuildingType);
            building.AssignedWorkerId = entity.AssignedWorkerId;
            building.X = entity.X;
            building.Y = entity.Y;
            building.Id = entity.Id;
            //building.AssignedWorkerId = entity.AssignedWorkerId;

            return building;
        }
        public static BuildingEntity ToEntity(Building domain)
        {
            return new BuildingEntity
            {
                BuildingType = domain.BuildingType,
                X = domain.X,
                Y = domain.Y,
                Id = domain.Id,
                AssignedWorkerId = domain.AssignedWorkerId
            };
        }
    }
}
