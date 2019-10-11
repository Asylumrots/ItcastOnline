using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.IBLL
{
    public interface ITestBLL:IBaseBLL<Test>
    {
        int GetIngTestNum();
        int GetEndTestNum();
    }
}
