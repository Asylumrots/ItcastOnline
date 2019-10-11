using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Itcast.DAL
{
    class Tool
    {
        //private static string constr = "server=.;integrated security=true;database=Itcast";
        private static string constr = "Data Source =.; Initial Catalog = Itcast; Integrated Security = True";
        /// <summary>
        /// 对数据进行更新的操作
        /// </summary>
        /// <param name="T_sql">要执行的T-sql语句</param>
        /// <returns>受影响的行数</returns>
        public static int Action(string T_sql)
        {
            SqlConnection sqlcon = new SqlConnection(constr);
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(T_sql, sqlcon);
            int n=sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            return n;
        }

        public static DataTable LoginTable(string sql, CommandType type, params SqlParameter[] pars)
        {
            using (SqlConnection conn = new SqlConnection(constr))
            {
                using (SqlDataAdapter apter = new SqlDataAdapter(sql, conn))
                {
                    if (pars != null)
                    {
                        apter.SelectCommand.Parameters.AddRange(pars);
                    }
                    apter.SelectCommand.CommandType = type;
                    DataTable da = new DataTable();
                    apter.Fill(da);
                    return da;
                }
            }
        }

        /// <summary>
        /// 对DataGridView进行更新数据
        /// </summary>
        /// <param name="T_sql">要执行的T-sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable getData(string T_sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(T_sql, constr);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        
        /// <summary>
        /// 判断一个字符数是否可以转换成数字
        /// </summary>
        /// <param name="num">需要转换的字符串</param>
        /// <returns>是否转换成功</returns>
        public static bool IsNum(string num)
        { 
            int n=0;
            bool key = int.TryParse(num, out n);
            return key;
        }

    }
}
