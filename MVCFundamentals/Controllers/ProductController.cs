using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFundamentals.Models;
using MVCFundamentals.ViewModels;
using System.Threading.Tasks;
namespace MVCFundamentals.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model = db.Products.OrderBy(x => x.ProductName).ToList();
                return View(model);
            }
            catch (Exception)
            {
                return View(new List<Product>());
            }
        }

        [HttpGet]
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
        public ActionResult Add()
        {
            var db = new MyNorthwindEntities();
            var model = new ProductViewModel()
            {
                Categories = db.Categories.ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductViewModel model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                db.Products.Add(model.Product);
                db.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Update(int? id)
        {
            var db = new MyNorthwindEntities();
            var model = new ProductViewModel()
            {
                Categories = db.Categories.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ProductViewModel model)
        {
            var db = new MyNorthwindEntities();
            var product = await db.Products.FindAsync(model.Product.ProductID);
            if (product == null)
                return RedirectToAction("Index", "Product");
            product.ProductName = model.Product.ProductName;
            product.CategoryID = model.Product.CategoryID;
            product.UnitPrice = model.Product.UnitPrice;
            product.UnitsInStock = model.Product.UnitsInStock;
            product.Discontinued = model.Product.Discontinued;
            await db.SaveChangesAsync();
            return RedirectToAction("Update","Product",new {id=model.Product.ProductID });
        }
    }
}