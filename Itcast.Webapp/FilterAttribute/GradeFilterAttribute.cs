using Itcast.Webapp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Itcast.Webapp.FilterAttribute
{
    //权限等级过滤器
    public class GradeFilterAttribute : ActionFilterAttribute
    {
        //在Action执行之前执行
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (BaseController.grade == 0)
            {
                filterContext.HttpContext.Response.Redirect("/Error/GradeError");
            }
        }
        ////在Action执行之后执行
        //public override void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    base.OnActionExecuted(filterContext);
        //    filterContext.HttpContext.Response.Write("<br />" + "执行OnActionExecuted：" + Message + "<br />");
        //}
        ////在Result执行之前执行
        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    base.OnResultExecuting(filterContext);
        //    filterContext.HttpContext.Response.Write("<br />" + "执行OnResultExecuting：" + Message + "<br />");
        //}
        ////在Result执行之后执行
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    base.OnResultExecuted(filterContext);
        //    filterContext.HttpContext.Response.Write("<br />" + "执行OnResultExecuted：" + Message + "<br />");
        //}
    }
}