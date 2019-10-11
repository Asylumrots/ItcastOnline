using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IDAL
{
    public interface IMsgDAL:IBaseDAL<Msg>
    {
        void AddUserEntities(int id, string txt, out int nums);
    }
}
