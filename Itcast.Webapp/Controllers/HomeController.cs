using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Itcast.Model;
using Itcast.BLL;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Itcast.IBLL;

namespace Itcast.Webapp.Controllers
{

    public class HomeController : BaseController //Base
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public ActionResult Index()
        {
            ViewData["User"] = Session["Userinfo"];
            UserInfo us = Session["Userinfo"] as UserInfo;
            IMsgBLL msgBLL = new MsgManager();
            ViewData["Msg"] = msgBLL.LoadEntity(u => u.ReceiveId == us.Id);
            //if (us.Grade==0)
            //{
            //    return Redirect("/Home/UserIndex");
            //}
            return View();
        }

        public ActionResult UserIndex()
        {
            return View();
        }

        public ActionResult Center()
        {
            TopicManager topicBLL = new TopicManager();
            ITestBLL testBLL = new TestManager();
            //设置数据
            ViewData["TopicNum"] = topicBLL.GetTopicInfoCount().ToString();
            ViewData["IngTestNum"] = testBLL.GetIngTestNum();
            ViewData["EndTestNum"] = testBLL.GetEndTestNum();
            return View();
        }

        public ActionResult Test()
        {
            ITestBLL testBLL = new TestManager();
            ViewData.Model = testBLL.LoadEntity(u => true);
            return View();
        }

        public ActionResult MyTest()
        {
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            ViewData.Model = userInfoBLL.LoadEntity(u => true);
            return View();
        }

        public ActionResult Exam()
        {
            int nums = Convert.ToInt32(Request["nums"]);
            //获得数据集合
            List<Topic> topics = GetTopic(nums);
            ViewData.Model = topics;
            return View();
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Message()
        {
            UserInfo user = Session["Userinfo"] as UserInfo;
            IMsgBLL msgBLL = new MsgManager();
            IUserInfoBLL userInfoBLL = new UserInfoManager();
            IEnumerable<Msg> model= msgBLL.LoadEntity(u => u.ReceiveId == user.Id);
            Dictionary<int, string> list = new Dictionary<int, string>();
            //没有数据返回Null
            try
            {
                if (model.First() == null)
                {
                }
            }
            catch
            {
                ViewData.Model = null;
                return View();
            }
            //序列化数据
            foreach (var item in model)
            {
                UserInfo us = userInfoBLL.LoadEntity(u => u.Id == item.SendId).FirstOrDefault();
                list[Convert.ToInt32(item.SendId)] = us.Name;
            }
            ViewData["NameList"] = list;
            ViewData.Model = model;
            return View();
        }

        public ActionResult Security()
        {
            return View();
        }

        public ActionResult TopicInfo()
        {
            ItcastEntities db = new ItcastEntities();
            //分页显示数据
            int pageIndex;
            int pageSize = 10;
            if (!int.TryParse(Request.QueryString["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            BLL.TopicManager examTopicManager = new BLL.TopicManager();
            //获得最大页数
            int pageCount = examTopicManager.GetpageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;

            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = pageCount;
            //获得数据集合
            var ExamTopic = examTopicManager.GetTopicInfo(PageIndex, PageSize);
            ViewData.Model = ExamTopic;
            ViewData["PageIndex"] = PageIndex;
            ViewData["PageCount"] = PageCount;
            return View();
        }

        public ActionResult GradeVlidate()
        {
            if (BaseController.grade >= 1)
            {
                return Content("ok");
            }
            else
            {
                Response.Redirect("GradeError");
                return Content("Error");
            }
        }
        public ActionResult TopicManager()
        {
            GradeVlidate();
            ItcastEntities db = new ItcastEntities();
            //分页显示数据
            int pageIndex;
            int pageSize = 10;
            if (!int.TryParse(Request.QueryString["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            BLL.TopicManager examTopicManager = new BLL.TopicManager();
            //获得最大页数
            int pageCount = examTopicManager.GetpageCount(pageSize);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;

            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = pageCount;
            //获得数据集合
            var ExamTopic = examTopicManager.GetTopicInfo(PageIndex, PageSize);
            ViewData.Model = ExamTopic;
            ViewData["PageIndex"] = PageIndex;
            ViewData["PageCount"] = PageCount;
            return View();
        }


        TopicManager examTopicManager = new TopicManager();
        //通过ID和选种的答案判断是否正确
        public bool SelectOption()
        {
            double TopicID = Convert.ToDouble(Request["TopicID"]);
            string selectOption = Request["SelectOption"];
            string trueoption = examTopicManager.SelectOption(TopicID, selectOption);
            if (trueoption.Equals(selectOption))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //通过传入的题目数量返回相应的随机题目
        public List<Topic> GetTopic(int nums)
        {
            List<Topic> topicList = examTopicManager.getTestTopicInfo(nums);
            return topicList;
        }

        //public class JsonEntity {
        //    public string topicId { get; set; }
        //    public string options { get; set; }
        //}

        //Dictionary<double, string> truenums = new Dictionary<double, string>();
        //IO获得ajax传递的json数据并和数据库比对判断正确性， 返回正确答案的json字符串
        [HttpPost]
        public ActionResult FullTopicTrue()
        {
            //int truenums = 0;
            Dictionary<string, string> trues = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                string stream = sr.ReadToEnd();
                //.Net内置读取json对象
                JavaScriptSerializer js = new JavaScriptSerializer();
                var list = js.Deserialize<Dictionary<string, string>>(stream);//反序列化
                foreach (var item in list)
                {
                    string trueoption = examTopicManager.SelectOption(Convert.ToDouble(item.Key), item.Value);
                    trues[item.Key] = trueoption;
                }
            }
            string s = SerializeDictionaryToJsonString<string, string>(trues);
            return Json(s);
            //double fenshu = truenums * (100 / nums);
            //return truenums;


            //double TopicID = Convert.ToDouble(Request["TopicID"]);
            //string selectOption = Request["SelectOption"];
            ////foreach (var item in d.Keys)
            ////{
            ////    Console.WriteLine(item + "----" + d[item]);
            ////}
            //if (!truenums.ContainsKey(TopicID))
            //{
            //    truenums.Add(TopicID, selectOption);
            //}
            //else
            //{
            //    truenums[TopicID] = selectOption;
            //}
            //return truenums;
        }

        //将字典集合转换成json字符串
        public static string SerializeDictionaryToJsonString<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            if (dict.Count == 0)
                return "";

            string jsonStr = JsonConvert.SerializeObject(dict);
            return jsonStr;
        }
    }
    
}