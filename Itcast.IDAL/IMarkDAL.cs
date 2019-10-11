using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IDAL
{
    public interface IMarkDAL:IBaseDAL<Mark>
    {
        Dictionary<string, string> GetNameAndScore(int id);
    }
}
