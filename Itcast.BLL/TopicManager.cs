using Itcast.DAL;
using Itcast.IBLL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itcast.BLL
{
    public class TopicManager:BaseManager<Topic>,ITopicBLL
    {
        public override void SetCurrentDAL()
        {
            CurrentDAL = this.CurrentBbSession.TopicDal;
        }
        TopicServices examTopicServices = new TopicServices();
        //获得题目数据条数总和
        public int GetTopicInfoCount()
        {
            return examTopicServices.GetTopicInfoCount();
        }
        //通过页数和大小计算起始位置和终止位置
        public List<Topic> GetTopicInfo(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            return examTopicServices.GetTopicInfo(start, end);
        }
        //通过每页大小计算页面数量
        public int GetpageCount(int pageSize)
        {
            int ScoreCount = examTopicServices.GetTopicInfoCount();
            int pageCount = Convert.ToInt32(Math.Ceiling((Double)ScoreCount / pageSize));
            return pageCount;
        }

        //通过ID和选项判断是否选择正确
        public string SelectOption(double TopciID, string selectOption)
        {
            string trueoption= examTopicServices.SelectOption(TopciID);
            return trueoption;
            //if (trueoption.Equals(selectOption))
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        //返回随机题目
        public List<Topic> getTestTopicInfo(int nums)
        {
            return examTopicServices.getTestTopicInfo(nums);
        }
    }
}
