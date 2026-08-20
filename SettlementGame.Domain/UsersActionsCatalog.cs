using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public static class UsersActionsCatalog
    {
        public static IUserAction CreateBuildingAction(CreateBuildingContext CreateBuildingContext)
        {
            return new CreateBuildingAction(CreateBuildingContext);
        }

        public static IUserAction HireWorkerAction(HireWorkerContext context)
        {
            return new HireWorkerAction(context);
        }

        public static IUserAction FireWorkerAction(FireWorkerContext context)
        {
            return new FireWorkerAction(context);
        }

    }
}
