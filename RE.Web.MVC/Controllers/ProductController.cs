using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RE.BLL.Repository;
using RE.BLL.Settings;
using RE.Models.Entities;

namespace RE.Web.MVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var urunler = new Repository.ProductRepo().Queryable().OrderBy(x=>x.ProductName).ToList();
            return View(urunler);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (!id.HasValue)
                    return RedirectToAction("Index", "Product");
                var urun = await new Repository.ProductRepo().GetByIdAsync(id.Value);
                if (urun == null)
                    return RedirectToAction("Index", "Product");
                return View(urun);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Güncelleme işleminde bir hata oluştu.");
                    return View(model);
                }
                var urun = await new Repository.ProductRepo().GetByIdAsync(model.ProductID);
                if (urun == null)
                {
                    ModelState.AddModelError("", "Ürün bulunamadı.");
                    return View(model);
                }

                urun.ProductName = model.ProductName;
                urun.UnitPrice = model.UnitPrice;
                urun.UnitsInStock = model.UnitsInStock;
                urun.Discontinued = model.Discontinued;               
                if (model.Foto != null && model.Foto.ContentLength > 0)
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.Foto.FileName);
                    string extensionName = Path.GetExtension(model.Foto.FileName);
                    fileName = SiteSettings.UrlFormatConverter(fileName);
                    fileName += Guid.NewGuid().ToString().Replace("-", "");
                    var directoryPath = Server.MapPath("~/Uploads/Products/");
                    var filePath = Server.MapPath("~/Uploads/Products/" + fileName + extensionName);
                    if (!Directory.Exists(directoryPath))
                        Directory.CreateDirectory(directoryPath);
                    model.Foto.SaveAs(filePath);
                    WebImage img=new WebImage(filePath);
                    img.Resize(800, 800,false);
                    img.AddTextWatermark("Ganyotçu Ticaret", fontColor: "Tomato", fontSize: 18, fontFamily: "Verdana");
                    img.Save(filePath);
                    if (!string.IsNullOrEmpty(urun.FotoUrl))
                        System.IO.File.Delete(Server.MapPath(urun.FotoUrl));
                    urun.FotoUrl = $@"/Uploads/Products/{fileName}{extensionName}";
                }
                await new Repository.ProductRepo().UpdateAsync();
                return RedirectToAction("Edit", "Product", new { urun.ProductID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Product");
            }
        }
    }
}