using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthAdmin.Models;

namespace NorthAdmin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Orders.OrderBy(x => x.OrderID).ToList();
                return View(model);
            }
            catch (Exception)
            {

                return View(new List<Order>());
            }
        }

        public ActionResult Detail(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Order_Details.Where(x => x.OrderID == id).ToList();
                ViewBag.OrderID = id;
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Order");
            }
        }
    }

}