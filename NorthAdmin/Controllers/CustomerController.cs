using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthAdmin.Models;

namespace NorthAdmin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Customers.OrderBy(x => x.CompanyName).ToList();
                return View(model);
            }
            catch (Exception)
            {

                return View(new List<Customer>());
            }
        }

        [HttpGet]
        public ActionResult Detail(string id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Customers.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Customer");
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Customer");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Customer model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var id = model.CustomerID.Trim();
                if (id.Length != 5)
                {
                    for (int i = 0; i < 5-id.Length; i++)
                    {
                        model.CustomerID += i;
                    }
                }
                db.Customers.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Customer");
            }
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Customers.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Customer");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Customer");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Customer model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var guncellenecek = db.Customers.Find(model.CustomerID);
                if (guncellenecek == null)
                    return RedirectToAction("Index", "Customer");
                guncellenecek.CompanyName = model.CompanyName;
                guncellenecek.ContactName = model.ContactName;
                guncellenecek.CustomerID = model.CustomerID;
                guncellenecek.City = model.City;
                guncellenecek.Country = model.Country;
                guncellenecek.Phone = model.Phone;
                db.SaveChanges();
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Customer");
            }
        }

        public ActionResult Delete(string id)
        {
            var db = new MyNorthwindEntities();
            var silinecek = db.Customers.Find(id);
            if (silinecek == null)
                return RedirectToAction("Index", "Customer");
            db.Customers.Remove(silinecek);
            db.SaveChanges();
            ViewBag.sonuc = $"{silinecek.CompanyName} isimli müşteri başarı ile silinmiştir.";
            return View();
        }
    }
}