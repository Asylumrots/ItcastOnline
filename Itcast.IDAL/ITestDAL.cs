using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Itcast.Model;

namespace Itcast.IDAL
{
    public interface ITestDAL : IBaseDAL<Test>
    {
        int GetIngTestNum();
        int GetEndTestNum();
    }
}
