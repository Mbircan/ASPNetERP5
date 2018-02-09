using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        #region Partial Results

        public PartialViewResult TopHeaderPartialResult()
        {
            return PartialView("_TopHeaderPartial");
        }

        public PartialViewResult SideMenuPartialResult()
        {
            return PartialView("_SideMenuPartial");
        }

        public PartialViewResult DropdownMessagesPartialResult()
        {
            return PartialView("_DropdownMessagesPartial");
        }
        public PartialViewResult DropdownTaskPartialResult()
        {
            return PartialView("_DropdownTaskPartial");
        }
        public PartialViewResult DropdownAlertsPartialResult()
        {
            return PartialView("_DropdownAlertsPartial");
        }
        public PartialViewResult DropdownUserPartialResult()
        {
            return PartialView("_DropdownUserPartial");
        }
        #endregion
    }
}