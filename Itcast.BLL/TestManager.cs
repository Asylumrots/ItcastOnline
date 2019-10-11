using Itcast.IBLL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.BLL
{
    public class TestManager : BaseManager<Test>, ITestBLL
    {
        public int GetEndTestNum()
        {
            return CurrentBbSession.TestDal.GetEndTestNum();
        }

        public int GetIngTestNum()
        {
            return CurrentBbSession.TestDal.GetIngTestNum();
        }

        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentBbSession.TestDal;
        }

        
    }
}
