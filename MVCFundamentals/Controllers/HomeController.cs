using MVCFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFundamentals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyNorthwindEntities db = new MyNorthwindEntities();
            var model = db.Categories.OrderBy(x=>x.CategoryName).ToList();

            ViewBag.durum = "Aktif";
            ViewBag.durum = true;
            ViewBag.durum = 1;
            return View(model);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            var db =new MyNorthwindEntities();
            var category = db.Categories.Find(id.Value);
            if (category == null)
                return RedirectToAction("Index", "Home");
            return View(category);
        }
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            var db = new MyNorthwindEntities();
            var product = db.Products.Find(id.Value);
            if (product == null)
                return RedirectToAction("Index", "Home");
            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}