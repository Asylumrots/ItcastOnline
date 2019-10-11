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
    public class MarkServices:BaseServices<Mark>,IMarkDAL
    {
        public Dictionary<string, string> GetNameAndScore(int id)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            string sql = "select UserInfo.Name,Mark.Score from Mark,UserInfo where Mark.UserId=UserInfo.Id and Mark.TestId='" + id + "'";
            DataTable dt = Tool.getData(sql);
            foreach (DataRow item in dt.Rows)
            {
                string name = item["Name"].ToString();
                string score = item["Score"].ToString();
                list[name] = score;
            }
            return list;
        }
    }
}
