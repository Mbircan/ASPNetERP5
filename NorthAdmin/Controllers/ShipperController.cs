using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthAdmin.Models;

namespace NorthAdmin.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
        public ActionResult Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Shippers.OrderBy(x => x.CompanyName).ToList();
                return View(model);
            }
            catch (Exception)
            {

                return View(new List<Shipper>());
            }
        }

        public ActionResult Detail(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Shippers.Find(id);
                if(model==null)
                    return RedirectToAction("Index", "Shipper");
                var orders = db.Orders.Where(x => x.ShipVia == id).ToList().Count();
                ViewBag.orders = orders;
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("Index","Shipper");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Shipper model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                db.Shippers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Shipper");
            }
            catch (Exception)
            {

                return RedirectToAction("Index","Shipper");
            }
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Shippers.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Shipper");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Shipper");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Shipper model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var guncellenecek = db.Shippers.Find(model.ShipperID);
                if (guncellenecek == null)
                    return RedirectToAction("Index", "Shipper");
                guncellenecek.CompanyName = model.CompanyName;
                guncellenecek.Phone = model.Phone;
                db.SaveChanges();
                return RedirectToAction("Index", "Shipper");
            }
            catch (Exception)
            {
                return RedirectToAction("Index","Shipper");
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Shippers.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Shipper");
                db.Shippers.Remove(model);
                db.SaveChanges();
                ViewBag.sonuc = $"{model.CompanyName} isimli kargo şirketi başarı ile silinmiştir.";
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Shipper");
            }
        }
    }
}