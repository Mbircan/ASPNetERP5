using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using RE.BLL.Repository;
using RE.Web.MVC.Models;

namespace RE.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GrafikData1()
        {
            var toplamUrunAdet = new Repository.ProductRepo().Queryable().Count();
            var kategoriler = await new Repository.CategoryRepo().GetAllAsync();
            List<CategoryReportModel> liste =new List<CategoryReportModel>();
            foreach (var item in kategoriler)
            {
                liste.Add(new CategoryReportModel()
                {
                    Ad= item.CategoryName,
                    Adet=item.Products.Count
                });
            }
            ViewBag.data1 = liste;
            return Json(liste,JsonRequestBehavior.AllowGet);
        }
    }
}