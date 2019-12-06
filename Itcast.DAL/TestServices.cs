using Itcast.IDAL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.DAL
{
    class TestServices : BaseServices<Test>, ITestDAL
    {
        public int GetEndTestNum()
        {
            string sql = "select count(*) from Test where TestEndTime<=GETDATE()";
            DataTable dt = Tool.getData(sql);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        public int GetIngTestNum()
        {
            string sql = "select count(*) from Test where TestEndTime>GETDATE()";
            DataTable dt = Tool.getData(sql);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
