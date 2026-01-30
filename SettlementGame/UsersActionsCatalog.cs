using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame
{
    internal class UsersActionsCatalog
    {   public static IUserAction Create(UsersActionType usersActionType, CreateBuildingContext createBuildingContext)
        {
            switch (usersActionType)
            {
                case UsersActionType.CreateBuilding:
                    return new CreateBuildingAction(createBuildingContext);

                //case UsersActionType.AssignFood:
                    //return new AssignFoodAction(context.ResourceType);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static UsersActionsCatalog GetUsersActionsCatalog(UsersActionType usersActions)
        {   
            switch (usersActions)
            {
                case UsersActionType.CreateBuilding:
                    //return new BuildingCatalog(ResourceType.Wood, 1, ResourceType.Doska, 5); //
                                                                                             //или вернуть тип потребляемого ресурса и его кол-во, тип производимого ресурса и его кол-во.
                //case BuildingType.StoneMakery:
                //    return new BuildingCatalog(ResourceType.Stone, 1, ResourceType.Kirpich, 4);
                //case BuildingType.GoldMakery:
                //    return new BuildingCatalog(ResourceType.Gold, 1, ResourceType.Moneta, 10);

                default: throw new ArgumentOutOfRangeException();
        //            createBuilding,
        //stopBuilding,
        //destroybuilding,
        //hairEmployee,
        //fireEmployee
            }
        }
    }
}
