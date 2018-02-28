using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kimlik.Web.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Users = "mbircan")]//Sadece 1 kişiye özel yapmak için.(Virgül ile kullanıcı eklenebilir.)
        public ActionResult SuperAdmin()
        {
            return View();
        }
    }
}