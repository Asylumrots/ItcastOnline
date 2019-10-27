using Itcast.Common;
using Itcast.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Itcast.Webapp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            //清空Session
            Session["Userinfo"] = null;
            return View();
        }

        //用户登陆
        public ActionResult UserLogin()
        {
            if (Request["validate"] == Session["validateCode"].ToString())
            {
                string name = Request["name"];
                string pwd = GetMd5.Get(Request["password"].ToString());
                //BLL.UserInfoManager userManager = new BLL.UserInfoManager();
                //UserInfo us = userManager.GetUserInfo(name, pwd);
                IBLL.IUserInfoBLL bLL = new BLL.UserInfoManager();
                UserInfo us = bLL.LoadEntity(u => u.Name == name && u.Pwd == pwd).FirstOrDefault();
                if (us != null)
                {
                    //赋值Session
                    Session["Userinfo"] = us;
                    //赋值用户权限，让所有继承于baseController的控制器都能取到用户权限进判断
                    BaseController.grade = us.Grade;
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
            else
            {
                return Content("validateError");
            }
            

        }

        //用户注册
        public ActionResult UserRegister()
        {
            //BLL.UserInfoManager userManager = new BLL.UserInfoManager();
            //UserInfo userinfo = new UserInfo();
            //userinfo.Name= Request["name"];
            //userinfo.Pwd= GetMd5.Get(Request["password1"].ToString());
            //userinfo.Email= Request["email"];
            //userinfo.RegTime = DateTime.Now;
            //bool register=userManager.UserRegister(userinfo);
            //if (register)
            //{
            //    return Content("registerYes");
            //}
            //else
            //{
            //    return Content("registerNo");
            //}
            IBLL.IUserInfoBLL bLL = new BLL.UserInfoManager();
            UserInfo userinfo = new UserInfo();
            userinfo.Name = Request["name"];
            userinfo.Pwd = GetMd5.Get(Request["password1"].ToString());
            userinfo.Email = Request["email"];
            userinfo.RegTime = DateTime.Now;
            bool register=bLL.AddEntity(userinfo);
            if (register)
            {
                return Content("registerYes");
            }
            else
            {
                return Content("registerNo");
            }
        }

        //获取验证码
        public ActionResult showValidate()
        {
            ValidateCode validate = new ValidateCode();
            var str = validate.CreateValidateCode(4);
            Session["validateCode"] = str;
            byte[]buffer = validate.CreateValidateGraphic(str);
            return File(buffer, "image/jpeg");

        }

        //找回密码发送邮件
        public void sendEmail(string emailAddress)
        {
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress("1773527072@qq.com", "统测系统管理员");//源邮件地址 
            mailMsg.To.Add(new MailAddress(emailAddress, "用户"));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "找回密码邮件";//发送邮件的标题 
            mailMsg.Body = "您的密码是：";//发送邮件的内容 
            SmtpClient client = new SmtpClient("smtp.qq.com");//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential("1773527072@qq.com", "123");
            client.Send(mailMsg);
        }
    }
}