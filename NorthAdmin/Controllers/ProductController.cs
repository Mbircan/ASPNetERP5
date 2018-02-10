using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using NorthAdmin.Models;

namespace NorthAdmin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var db = new MyNorthwindEntities();
            var model = db.Products.OrderBy(x => x.ProductName).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                var kategorilist = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Kategori Seçiniz",
                    Value = null
                }

            };
                kategorilist.Add(new SelectListItem()
                {
                    Text = "Yok",
                    Value = null
                });
                var db = new MyNorthwindEntities();
                db.Categories
                   .OrderBy(x => x.CategoryName)
                   .ToList()
                   .ForEach(x =>
                {
                    kategorilist.Add(new SelectListItem()
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryID.ToString()
                    });
                });
                var tedarikcilist = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text = "Tedarikçi Seçiniz",
                        Value = null
                    }

                };
                tedarikcilist.Add(new SelectListItem()
                {
                    Text = "Yok",
                    Value = null
                });
                db.Suppliers
                  .OrderBy(x => x.CompanyName)
                  .ToList()
                  .ForEach(x =>
                    {
                        tedarikcilist.Add(new SelectListItem()
                        {
                            Text = x.CompanyName,
                            Value = x.SupplierID.ToString()
                        });
                    });
                ViewBag.kategorilist = kategorilist;
                ViewBag.tedarikcilist = tedarikcilist;
                return View();
            }
            catch (Exception)
            {

                return RedirectToAction("Index","Product");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Product");
            }
        }

        public ActionResult Detail(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Products.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Product");
                return View(model);
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Product");
            }
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var kategorilist = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Kategori Seçiniz",
                    Value = null
                }

            };
            var db = new MyNorthwindEntities();
            db.Categories
                .OrderBy(x => x.CategoryName)
                .ToList()
                .ForEach(x =>
                {
                    kategorilist.Add(new SelectListItem()
                    {
                        Text = x.CategoryName,
                        Value = x.CategoryID.ToString()
                    });
                });
            var tedarikcilist = new List<SelectListItem>();
            db.Suppliers
                .OrderBy(x => x.CompanyName)
                .ToList()
                .ForEach(x =>
                {
                    tedarikcilist.Add(new SelectListItem()
                    {
                        Text = x.CompanyName,
                        Value = x.SupplierID.ToString()
                    });
                });
            ViewBag.kategorilist = kategorilist;
            ViewBag.tedarikcilist = tedarikcilist;
            var model = db.Products.Find(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Product model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var product = db.Products.Find(model.ProductID);
                if(product==null)
                    return RedirectToAction("Index", "Product");
                product.ProductName = model.ProductName;
                product.SupplierID = model.SupplierID;
                product.CategoryID = model.CategoryID;
                product.UnitPrice = model.UnitPrice;
                product.Discontinued = model.Discontinued;
                product.UnitsInStock = model.UnitsInStock;
                db.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            catch (Exception)
            {
                return  RedirectToAction("Index","Product");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var silinecek = db.Products.Find(id);
                if (silinecek == null)
                    return RedirectToAction("Index", "Product");
                db.Products.Remove(silinecek);
                db.SaveChanges();
                ViewBag.sonuc= ViewBag.sonuc = $"{silinecek.ProductName} isimli ürün başarı ile silinmiştir.";
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Product");
            }
        }
    }
}