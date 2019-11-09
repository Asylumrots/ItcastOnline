using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Itcast.Webapp.Controllers
{
    public class ErrorController : Controller
    {
        //未登录错误视图
        public ActionResult LoginError()
        {
            return View();
        }

        //权限错误视图
        public ActionResult GradeError()
        {
            return View();
        }
    }
}