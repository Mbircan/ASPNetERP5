using LayoutKullanimi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayoutKullanimi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.mesaj = "Hello Layout";
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public PartialViewResult HeaderPartialResult()
        {
            var db = new MyNorthwindEntities();
            var model = db.Products.ToList();
            return PartialView("_HeaderPartial",model);
        }
    }
}