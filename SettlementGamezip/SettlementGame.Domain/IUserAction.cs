using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SettlementGame.Domain
{
    public interface IUserAction
    {
        void Execute(DataWorld world);
    }
}
