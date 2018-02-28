using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BundleFilterConfig.Services
{
    public class ExceptionHandlerFilterAttribute:ActionFilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(filterContext.ExceptionHandled||!filterContext.HttpContext.IsCustomErrorEnabled)
                return;
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    ContentType = "application/json",
                    Data = new
                    {
                        name=filterContext.Exception.GetType().Name,
                        message=filterContext.Exception.Message,
                        trace=filterContext.Exception.StackTrace,
                        innerException=filterContext.Exception.InnerException
                    },
                    JsonRequestBehavior =JsonRequestBehavior.AllowGet
                };
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}