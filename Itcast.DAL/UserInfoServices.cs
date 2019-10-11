using Itcast.Model;
using Itcast.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.DAL
{
    public class UserInfoServices:BaseServices<UserInfo>,IUserInfoDAL
    {
        
        //根据name获取用户数据
        public UserInfo GetUserInfo(string name)
        {
            string sql = "select * from UserInfo where name=@name";
            SqlParameter[] pars = {
                new SqlParameter("@name",SqlDbType.VarChar)
            };
            pars[0].Value = name;
            DataTable dt= Tool.LoginTable(sql,CommandType.Text,pars);
            UserInfo us = null;
            if (dt.Rows.Count>0)
            {
                us = new UserInfo();
                us.Id = Convert.ToInt32(dt.Rows[0]["id"]);
                us.Name = dt.Rows[0]["name"].ToString();
                us.Pwd = dt.Rows[0]["pwd"].ToString();
                us.Email = dt.Rows[0]["email"].ToString();
                us.RegTime = Convert.ToDateTime(dt.Rows[0]["regTime"]);
            }
            return us;
        }
    }
}
