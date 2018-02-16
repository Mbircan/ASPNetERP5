using SepetUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SepetUygulamasi.Controllers
{
    public class SiparisController : Controller
    {
        // GET: Siparis
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult Urunler()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var urunler = db.Products.Select(x => new
                {
                    x.ProductID,
                    x.ProductName,
                    x.Categories.CategoryName,
                    x.UnitPrice,
                    x.UnitsInStock
                }).ToList();
                return Json(new ResponseModel()
                {
                    data = urunler,
                    success = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel()
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Urunler(List<SepetViewModel> model)
        {
            var db = new MyNorthwindEntities();
            using (var tran = db.Database.BeginTransaction())
            {
                try
                {
                    var siparis = new Orders()
                    {
                        EmployeeID = db.Employees.First().EmployeeID,
                        CustomerID = db.Customers.First().CustomerID,
                        Freight = 20,
                        OrderDate = DateTime.Now,
                        RequiredDate = DateTime.Now.AddDays(9),
                        ShipCity = "İstanbul",
                        ShipVia = db.Shippers.First().ShipperID
                    };
                    db.Orders.Add(siparis);
                    db.SaveChanges();
                    foreach (var item in model)
                    {
                        db.Order_Details.Add(new Order_Details()
                        {
                            OrderID = siparis.OrderID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            Discount = 0
                        });
                    }
                    db.SaveChanges();
                    tran.Commit();
                    return Json(new ResponseModel()
                    {
                        success = true,
                        message =$"{siparis.OrderID} nolu siparişiniz onaylanmıştır."
                    }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return Json(new ResponseModel()
                    {
                        success = false,
                        message = ex.Message
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}