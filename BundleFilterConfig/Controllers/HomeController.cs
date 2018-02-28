using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BundleFilterConfig.Services;

namespace BundleFilterConfig.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [HandleError(View = "Error")]
        [ExceptionHandlerFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}