using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Itcast.Webapp.Controllers
{
    public class BaseController : Controller
    {
        //设置全局权限变量
        public static int grade=0;
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //统一校验登陆
            if (Session["Userinfo"]==null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                //返回Result不用执行控制器方法代码返回ActionResult提高性能
                filterContext.Result = Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 验证用户权限
        /// </summary>
        /// <returns>True return “ok” ： False return “Error”</returns>
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
    }
}