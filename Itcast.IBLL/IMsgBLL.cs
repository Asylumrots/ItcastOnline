using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IBLL
{
    public interface IMsgBLL:IBaseBLL<Msg>
    {
        bool AddUserEntities(int id,string txt, out int nums);
    }
}
