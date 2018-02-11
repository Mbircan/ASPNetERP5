using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthAdmin.Models;

namespace NorthAdmin.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Suppliers.OrderBy(x => x.CompanyName).ToList();
                return View(model);
            }
            catch (Exception)
            {

                return View(new List<Supplier>());
            }
        }

        [HttpGet]
        public ActionResult Detail(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Suppliers.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Supplier");
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Supplier");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Supplier model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                db.Suppliers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Supplier");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Supplier");
            }
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Suppliers.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Supplier");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Supplier");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Supplier model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var guncellenecek = db.Suppliers.Find(model.SupplierID);
                if (guncellenecek == null)
                    return RedirectToAction("Index", "Supplier");
                guncellenecek.CompanyName = model.CompanyName;
                guncellenecek.ContactName = model.ContactName;
                guncellenecek.City = model.City;
                guncellenecek.Country = model.Country;
                guncellenecek.Phone = model.Phone;
                db.SaveChanges();
                return RedirectToAction("Index", "Supplier");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Supplier");
            }
        }

        public ActionResult Delete(int? id)
        {
            var db = new MyNorthwindEntities();
            var silinecek = db.Suppliers.Find(id);
            if (silinecek == null)
                return RedirectToAction("Index", "Supplier");
            db.Suppliers.Remove(silinecek);
            db.SaveChanges();
            ViewBag.sonuc = $"{silinecek.CompanyName} isimli tedarikçi başarı ile silinmiştir.";
            return View();
        }
    }
}