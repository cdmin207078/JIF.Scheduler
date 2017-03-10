using JIF.Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JIF.Scheduler.Web
{
    public class JIFExceptionAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JIFException)
            {
                if (context.HttpContext.Request.IsAjaxRequest())
                {
                    context.Result = new JsonResult
                    {
                        ContentEncoding = Encoding.UTF8,
                        ContentType = "application/json",
                        Data = new
                        {
                            success = false,
                            message = context.Exception.Message,
                            data = ""
                        }
                    };
                }
                else
                {
                    var referrer = context.RequestContext.HttpContext.Request.UrlReferrer.ToString();

                    // http://www.cnblogs.com/lindaWei/archive/2013/01/15/2860028.html - MVC3中 ViewBag、ViewData和TempData的使用和区别
                    // http://www.cnblogs.com/tristanguo/archive/2009/04/06/1430062.html - Asp.Net Mvc: 浅析TempData机制
                    //context.Controller.ViewBag._JIFExceptionMessage = context.Exception.Message;  -- 无效
                    context.Controller.TempData["JIFExceptionMessage"] = context.Exception.Message;

                    context.Result = new RedirectResult(referrer);
                }

                context.ExceptionHandled = true;
            }
        }
    }


    public class AppExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
        }
    }
}