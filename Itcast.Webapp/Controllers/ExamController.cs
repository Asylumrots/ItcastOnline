using Itcast.BLL;
using Itcast.IBLL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Itcast.Webapp.Controllers
{
    public class ExamController : BaseController
    {
        // GET: Exam
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Request["id"]);
            UserInfo us = Session["Userinfo"] as UserInfo;
            //验证是否进入过该统测
            IMarkBLL markBLL = new MarkManager();
            var model = markBLL.LoadEntity(u => u.TestId == id && u.UserId==us.Id);
            if (model.FirstOrDefault() != null)
            {
                return Content("<script>alert('您已经参加过本次统测！');history.go(-1);</script>");//不刷新退回页面
            }
            TopicManager topicBLL = new TopicManager();
            ITestBLL testBLL = new TestManager();
            IQueryable<Test> teslist = testBLL.LoadEntity(u => u.TestId == id);
            Test test = new Test();
            foreach (var item in teslist)
            {
                test.TestNum = item.TestNum;
            }
            //获得数据集合 
            List<Topic> topics = topicBLL.getTestTopicInfo(test.TestNum);
            ViewData.Model = topics;
            ViewData["nums"] = test.TestNum.ToString(); ;
            ViewData["TestId"] = id;
            
            ViewData["UserId"] = us.Id;
            return View();
        }

        //查看分数
        public ActionResult Score()
        {
            if (BaseController.grade >= 1)
            {
                IMarkBLL markBLL = new MarkManager();
                int id = Convert.ToInt32(Request["id"]);
                ViewData.Model= markBLL.GetNameAndScore(id);
                return View();
            }
            else
            {
                Response.Redirect("GradeError");
                return Content("Error");
            }
            
        }

        public ActionResult SaveScore()
        {
            int testId = Convert.ToInt32(Request["TestId"].ToString());
            int UserId = Convert.ToInt32(Request["UserId"].ToString());
            var ss = Request["Score"].ToString() ;
            int score = Convert.ToInt32(Request["Score"]);
            IMarkBLL markBLL = new MarkManager();
            Mark mark = new Mark();
            mark.TestId = testId;
            mark.UserId = UserId;
            mark.Score = score;
            mark.SubmitDate = DateTime.Now;
            if (!markBLL.AddEntity(mark))
            {
                return Content("no");
            }
            return Content("ok");
        }
    }
}