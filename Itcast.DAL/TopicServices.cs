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
    public class TopicServices:BaseServices<Topic>,ITopicDAL
    {
        //获得起始位置和终止位置之间的题目数据
        public List<Topic> GetTopicInfo(int start, int end)
        {
            string sql = "select * from (select *,rn=ROW_NUMBER() over(order by TopicID asc) from Topic) as temp where temp.rn between " + start + " and " + end + "";
            DataTable dt = Tool.getData(sql);
            List<Topic> list = null;
            if (dt.Rows.Count > 0)
            {
                list = new List<Topic>();
                foreach (DataRow row in dt.Rows)
                {
                    Topic topic = new Topic();
                    topic.TopicID = Convert.ToInt32(row["TopicID"]);
                    topic.Title = row["Title"].ToString();
                    topic.AnswerA = row["AnswerA"].ToString();
                    topic.AnswerB = row["AnswerB"].ToString();
                    topic.AnswerC = row["AnswerC"].ToString();
                    topic.AnswerD = row["AnswerD"].ToString();
                    topic.Answer = row["Answer"].ToString();
                    list.Add(topic);
                }
            }
            return list;
        }
        //获得题目数据条数总和
        public int GetTopicInfoCount()
        {
            string sql = "select count(*) from Topic";
            DataTable dt = Tool.getData(sql);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        //通过ID查询正确的答案
        public string  SelectOption(double TopciID)
        {
            string sql = "select Answer from Topic where TopiciD='" + TopciID + "'";
            DataTable dt = Tool.getData(sql);
            return dt.Rows[0][0].ToString();
        }
        //通过题目数量返回对应的随机题目
        public List<Topic> getTestTopicInfo(int nums)
        {
            string sql = "select top "+nums+"  * from  Topic order  by newid()";
            DataTable dt= Tool.getData(sql);
            List<Topic> topics = new List<Topic>();
            foreach (DataRow dr in dt.Rows)
            {
                Topic topic = new Topic();
                topic.TopicID = Convert.ToInt32(dr["TopicID"]);
                topic.Title = dr["Title"].ToString();
                topic.AnswerA = dr["AnswerA"].ToString();
                topic.AnswerB = dr["AnswerB"].ToString();
                topic.AnswerC = dr["AnswerC"].ToString();
                topic.AnswerD = dr["AnswerD"].ToString();
                topic.Answer = dr["Answer"].ToString();
                topic.CourseID = Convert.ToInt32(dr["CourseID"]);
                topics.Add(topic);
            }
            return topics;
        }
    }
}
