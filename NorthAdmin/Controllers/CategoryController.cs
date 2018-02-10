using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthAdmin.Models;

namespace NorthAdmin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Categories.OrderBy(x => x.CategoryName).ToList();
                return View(model);
            }
            catch (Exception)
            {
                return View(new List<Category>());
            }
        }
        [HttpGet]
        public ActionResult Detail(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Categories.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Home");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Category");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                //db.Categories.Add(new Category()
                //{
                //    CategoryName = model.CategoryName,
                //    Description = model.Description
                //});
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Categories.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Home");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Category");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Category model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var category = db.Categories.Find(model.CategoryID);
                if (category == null)
                    return RedirectToAction("Index", "Category");
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;
                db.SaveChanges();
                return RedirectToAction("Update", "Category", new { id = category.CategoryID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Category");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var silinecek = db.Categories.Find(id);
                if (silinecek == null)
                    return RedirectToAction("Index", "Category");
                db.Categories.Remove(silinecek);
                db.SaveChanges();
                ViewBag.sonuc = $"{silinecek.CategoryName} isimli kategori başarı ile silinmiştir.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = $"Kategori silme işleminde bir hata oluştu.<br/>{ex.Message}";
                return View();
            }
        }
    }
}