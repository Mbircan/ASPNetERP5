using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCFundamentals.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MVCFundamentals.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model =await db.Employees.OrderBy(x => x.FirstName).ThenBy(x=>x.LastName).ToListAsync();
                return View(model);
            }
            catch (Exception)
            {
                return View(new List<Employee>());
            }
        }

        [HttpGet]
        public async Task<ActionResult> Detail(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var model =await db.Employees.FindAsync(id);
                if (model == null)
                    return RedirectToAction("Index", "Employee");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Employee");
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            var calisanlarlist = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                Text="Amiri Yok",
                Value="null"
                }
            };
            var db = new MyNorthwindEntities();
            db.Employees
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList()
                .ForEach(x => {
                    calisanlarlist.Add(new SelectListItem()
                    {
                        Text = $"{x.FirstName} {x.LastName} - {x.Title}",
                        Value = x.EmployeeID.ToString()
                    });
                });
            ViewBag.calisanlarlist = calisanlarlist;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Employee model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                //db.Employees.Add(new Employee()
                //{
                //    FirstName = model.FirstName,
                //    Address = model.Address,
                //    BirthDate = model.BirthDate,
                //    City = model.City,
                //    Country = model.Country,
                //    LastName = model.LastName,
                //    HireDate = model.HireDate,
                //});
                db.Employees.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Employee");
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
                var calisanlarlist = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                Text="Amiri Yok",
                Value="null"
                }
            };
                var db = new MyNorthwindEntities();
                db.Employees
                    .OrderBy(x => x.FirstName)
                    .ThenBy(x => x.LastName)
                    .ToList()
                    .ForEach(x => {
                        calisanlarlist.Add(new SelectListItem()
                        {
                            Text = $"{x.FirstName} {x.LastName} - {x.Title}",
                            Value = x.EmployeeID.ToString()
                        });
                    });
                ViewBag.calisanlarlist = calisanlarlist;
                db = new MyNorthwindEntities();
                var model = db.Employees.Find(id);
                if (model == null)
                    return RedirectToAction("Index", "Employee");
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Employee");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Employee model)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var employee = db.Employees.Find(model.EmployeeID);
                if (employee == null)
                    return RedirectToAction("Index", "Employee");
                employee.FirstName = model.FirstName;
                employee.Address = model.Address;
                employee.BirthDate = model.BirthDate;
                employee.City = model.City;
                employee.Country = model.Country;
                employee.LastName = model.LastName;
                employee.HireDate = model.HireDate;
                db.SaveChanges();
                return RedirectToAction("Update", "Employee", new { id = employee.EmployeeID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index", "Employee");
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var silinecek = db.Employees.Find(id);
                if (silinecek == null)
                    return RedirectToAction("Index", "Employee");
                db.Employees.Remove(silinecek);
                db.SaveChanges();
                ViewBag.sonuc = $"{silinecek.FirstName} {silinecek.LastName} isimli çalışan başarı ile silinmiştir.";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = $"Çalışan silme işleminde bir hata oluştu.<br/>{ex.Message}";
                return View();
            }
        }
    }
}