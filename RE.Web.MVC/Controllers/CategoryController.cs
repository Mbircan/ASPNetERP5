using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RE.BLL.Repository;
using RE.Models.Entities;

namespace RE.Web.MVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public async Task<ActionResult> Index()
        {
            var kategoriler = await new Repository.CategoryRepo().GetAllAsync();
            return View(kategoriler);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return RedirectToAction("Index", "Category");
                var kategori = await new Repository.CategoryRepo().GetByIdAsync(id.Value);
                if (kategori == null)
                    return RedirectToAction("Index", "Category");
                return View(kategori);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("","Güncelleme işleminde bir hata oluştu.");
                    return View(model);
                }
                var kategori = await new Repository.CategoryRepo().GetByIdAsync(model.CategoryID);
                if (kategori == null)
                {
                    ModelState.AddModelError("", "Kategori bulunamadı.");
                    return View(model);
                }
                kategori.CategoryName = model.CategoryName;
                kategori.Description = model.Description;
                await new Repository.CategoryRepo().UpdateAsync();
                return RedirectToAction("Edit", "Category",new {kategori.CategoryID});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Category");
            }
        }

    }
}