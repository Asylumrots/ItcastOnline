using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IBLL
{
    public interface IMarkBLL:IBaseBLL<Mark>
    {
        Dictionary<string, string> GetNameAndScore(int id);
    }
}
