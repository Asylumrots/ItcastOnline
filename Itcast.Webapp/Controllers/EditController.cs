using Itcast.BLL;
using Itcast.IBLL;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Itcast.Common;
using Itcast.Webapp.FilterAttribute;

namespace Itcast.Webapp.Controllers
{
    public class EditController : BaseController
    {
        // GET: Edit
        //统测视图
        [GradeFilter]
        public ActionResult Test()
        {
            return View();
        }

        [GradeFilter]
        public ActionResult AddTest()
        {
            ITestBLL testBLL = new TestManager();
            Test test = new Test();
            test.TestName = Request["TestName"];
            test.TestNum = Convert.ToInt32(Request["TestNum"]);
            test.TestEndTime = Convert.ToDateTime(Request["TestEndTime"]);
            var time = DateTime.Now.ToLocalTime();
            if (test.TestEndTime <= DateTime.Now.ToLocalTime())
            {
                return Content("<script>alert('结束时间不能小于当前时间！');history.go(-1);</script>");//不刷新退回页面
            }
            if (testBLL.AddEntity(test))
            {
                return Redirect("/Home/Test");
            }
            else
            {
                return Content("<script>alert('添加失败！');window.location.href='/Home/AddTest';</script>");//刷新页面
            }
        }

        [GradeFilter]
        public ActionResult DelTest()
        {
            ITestBLL testBLL = new TestManager();
            var id = Convert.ToInt32(Request["id"]);
            Test test = testBLL.LoadEntity(u => u.TestId == id).FirstOrDefault();
            if (testBLL.DeleteEntity(test))
            {
                return Redirect("/Home/Test");
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='/Home/Test';</script>");//刷新页面
            }
        }

        public ActionResult ModifyUserInfo()
        {
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            UserInfo inu = Session["Userinfo"] as UserInfo;
            UserInfo us = userInfoBLL.LoadEntity(u => u.Id == inu.Id).FirstOrDefault();
            //us.Id = inu.Id;
            //us.Name = inu.Name;
            //us.Grade = inu.Grade;
            //us.RegTime = inu.RegTime;
            us.Sex = Request["sex"];
            us.Age = Convert.ToInt32(Request["age"]);
            us.Email = Request["email"];
            if (userInfoBLL.ModifyEntity(us))
            {
                Session["Userinfo"] = us;
                return Content("<script>alert('修改成功！');window.location.href='/Home/Info';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('修改失败！');history.go(-1);</script>");//不刷新退回页面
            }
        }

        public ActionResult ModifyUserPwd()
        {
            if (Request["NewPwd"] != Request["NewPwd2"])
            {
                return Content("<script>alert('两次输入的密码必须一致！');history.go(-1);</script>");//不刷新退回页面
            }
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            UserInfo inu = Session["Userinfo"] as UserInfo;
            UserInfo us = userInfoBLL.LoadEntity(u => u.Id == inu.Id).FirstOrDefault();
            string OldPwd = us.Pwd;
            string inputOldPwd = GetMd5.Get(Request["OldPwd"]);
            if (OldPwd.Equals(inputOldPwd))
            {
                us.Pwd = GetMd5.Get(Request["NewPwd"]);
                if (userInfoBLL.ModifyEntity(us))
                {
                    return Content("<script>alert('修改成功！请重新登陆');window.location.href='/Login/Index';</script>");//刷新页面
                }
                else
                {
                    return Content("<script>alert('修改失败！');window.location.href='/Home/Security';</script>");//刷新页面
                }
            }
            return Content("<script>alert('系统繁忙！');history.go(-1);</script>");//不刷新退回页面
        }

        public ActionResult DelMsg()
        {
            int id = Convert.ToInt32(Request["id"]);
            IMsgBLL msgBLL = new MsgManager();
            Msg msg = msgBLL.LoadEntity(u => u.MsgId == id).FirstOrDefault();
            if (msgBLL.DeleteEntity(msg))
            {
                return Content("<script>window.location.href='/Home/Message';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('删除失败！');window.location.href='/Home/Message';</script>");//刷新页面
            }
        }

        [GradeFilter]
        public ActionResult SendMsg()
        {
            string name = Request["SendName"];
            string txt = Request["SendTxt"];
            if (name.Equals("*"))
            {
                IUserInfoBLL userInfoBLL = new UserInfoManager();
                IMsgBLL msgBLL = new MsgManager();
                UserInfo inu = Session["Userinfo"] as UserInfo;
                int nums = 0;
                if (msgBLL.AddUserEntities(inu.Id, txt, out nums))
                {
                    return Content("<script>alert('成功发送" + nums + "条消息');window.location.href='/Home/Message';</script>");//刷新页面
                }
                else
                {
                    return Content("<script>alert('发送失败！');history.go(-1);</script>");//不刷新退回页面
                }
            }
            else
            {
                IUserInfoBLL userInfoBLL = new UserInfoManager();
                IMsgBLL msgBLL = new MsgManager();
                UserInfo inu = Session["Userinfo"] as UserInfo;
                UserInfo us = userInfoBLL.LoadEntity(u => u.Name == name).FirstOrDefault();
                Msg msg = new Msg();
                msg.SendId = inu.Id;
                msg.ReceiveId = us.Id;
                msg.MsgTxt = txt;
                msg.MsgSendTime = DateTime.Now.ToLocalTime();
                if (msgBLL.AddEntity(msg))
                {
                    return Content("<script>alert('发送成功！');window.location.href='/Home/Message';</script>");//刷新页面
                }
                else
                {
                    return Content("<script>alert('发送失败！');history.go(-1);</script>");//不刷新退回页面
                }
            }
        }

        //视图
        [GradeFilter]
        public ActionResult AddTopic()
        {
            return View();
        }

        [GradeFilter]
        public ActionResult AddTopicMethod()
        {
            Topic topic = new Topic();
            topic.TopicID = Convert.ToInt32(Request["TopicId"]);
            topic.Title = Request["TopicTitle"];
            topic.AnswerA = Request["AnswerA"];
            topic.AnswerB = Request["AnswerB"];
            topic.AnswerC = Request["AnswerC"];
            topic.AnswerD = Request["AnswerD"];
            topic.Answer = Request["Answer"];
            topic.CourseID = Convert.ToInt32(Request["CourseID"]);
            ITopicBLL topicBLL = new TopicManager();
            if (topicBLL.AddEntity(topic))
            {
                return Content("<script>alert('添加成功！');window.location.href='/Home/TopicManager';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('添加失败！');history.go(-1);</script>");//不刷新退回页面
            }
        }

        [GradeFilter]
        public ActionResult DelTopicMethod()
        {
            int id = Convert.ToInt32(Request["id"]);
            ITopicBLL topicBLL = new TopicManager();
            Topic topic = topicBLL.LoadEntity(u => u.TopicID == id).FirstOrDefault();
            if (topicBLL.DeleteEntity(topic))
            {
                return Content("<script>alert('删除成功！');window.location.href='/Home/TopicManager';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('删除失败！');history.go(-1);</script>");//不刷新退回页面
            }
        }

        //修改试题的视图
        [GradeFilter]
        public ActionResult ModifyTopic()
        {
            int id = Convert.ToInt32(Request["id"]);
            ITopicBLL topicBLL = new TopicManager();
            Topic topic = topicBLL.LoadEntity(u => u.TopicID == id).FirstOrDefault();
            ViewData["Topic"] = topic;
            return View();
        }

        [GradeFilter]
        public ActionResult ModifyTopicMethod()
        {
            int id = Convert.ToInt32(Request["id"]);
            ITopicBLL topicBLL = new TopicManager();
            Topic topic = topicBLL.LoadEntity(u => u.TopicID == id).FirstOrDefault();
            topic.TopicID = Convert.ToInt32(Request["TopicId"]);
            topic.Title = Request["TopicTitle"];
            topic.AnswerA = Request["AnswerA"];
            topic.AnswerB = Request["AnswerB"];
            topic.AnswerC = Request["AnswerC"];
            topic.AnswerD = Request["AnswerD"];
            topic.Answer = Request["Answer"];
            if (topicBLL.ModifyEntity(topic))
            {
                return Content("<script>alert('修改成功！');window.location.href='/Home/TopicManager';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('修改失败！');history.go(-1);</script>");//不刷新退回页面
            }
        }

        [GradeFilter]
        public ActionResult ModifyUser()
        {
            int id = Convert.ToInt32(Request["id"]);
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            UserInfo user = userInfoBLL.LoadEntity(u => u.Id == id).FirstOrDefault();
            ViewData["UserInfo"] = user;
            return View();
        }

        [GradeFilter]
        public ActionResult ModifyUserMethod()
        {
            int id = Convert.ToInt32(Request["id"]);
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            UserInfo user = userInfoBLL.LoadEntity(u => u.Id == id).FirstOrDefault();
            if (Request["UserName"]==""|| Request["RegTime"]=="" || Request["UserGrade"]=="")
            {
                return Content("<script>alert('修改的数据不能为空！');history.go(-1);</script>");//不刷新退回页面
            }
            if (Request["UserSex"] != "")
            {
                user.Sex = Request["UserSex"];
            }
            if (Request["UserAge"] != "")
            {
                user.Sex = Request["UserAge"];
            }
            user.Name = Request["UserName"];
            user.RegTime = Convert.ToDateTime(Request["RegTime"]);
            user.Grade = Convert.ToInt32(Request["UserGrade"]);
            if (userInfoBLL.ModifyEntity(user))
            {
                return Content("<script>alert('修改成功！');window.location.href='/Home/MyTest';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('修改失败！');history.go(-1);</script>");//不刷新退回页面
            }
        }

        [GradeFilter]
        public ActionResult DelUserMethod()
        {
            int id = Convert.ToInt32(Request["id"]);
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            UserInfo us = userInfoBLL.LoadEntity(u => u.Id == id).FirstOrDefault();
            if (userInfoBLL.DeleteEntity(us))
            {
                return Content("<script>alert('删除成功！');window.location.href='/Home/MyTest';</script>");//刷新页面
            }
            else
            {
                return Content("<script>alert('删除失败！');history.go(-1);</script>");//不刷新退回页面
            }
        }

        
    }
}