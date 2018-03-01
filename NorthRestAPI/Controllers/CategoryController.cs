using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NorthRestAPI.Models;
using NorthRestAPI.Models.ViewModels;
using Exception = System.Exception;

namespace NorthRestAPI.Controllers
{
    public class CategoryController : ApiController
    {
        [HttpGet]
        public ResponseData GetAll()
        {
            try
            {
                var db=new MyNorthwindEntities();
                var kategoriler = db.Categories.Select(x=>new
                {
                    x.CategoryID,
                    x.CategoryName,
                    x.Description,
                    ProductsCount=x.Products.Count
                }).ToList();
                return new ResponseData()
                {
                    Success = true,
                    Data = kategoriler
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        [HttpGet]
        public ResponseData GetById(int id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var kategori = db.Categories.Find(id);
                if (kategori == null)
                {
                    return new ResponseData()
                    {
                        Message = "Kategori Bulunamadı",
                        Success = false
                    };
                }
                return new ResponseData()
                {
                    Success = true,
                    Data = new
                    {
                        kategori.CategoryID,
                        kategori.CategoryName,
                        kategori.Description,
                        ProductsCount=kategori.Products.Count
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        [HttpPost]
        public async Task<ResponseData> Insert([FromBody] CategoryViewModel value)
        {
            try
            {
                var db=new MyNorthwindEntities();
                db.Categories.Add(new Category()
                {
                    CategoryName = value.CategoryName,
                    Description = value.Description
                });
               await db.SaveChangesAsync();
               return new ResponseData()
                {
                    Success = true,
                    Message = $"{value.CategoryName} isimli kategori başarı ile eklenmiştir."
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        public async Task<ResponseData> Update(int id, [FromBody] CategoryViewModel value)
        {
            try
            {
                var db=new MyNorthwindEntities();
                var category = db.Categories.Find(id);
                if (category == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı."
                    };
                }

                category.CategoryName = value.CategoryName;
                category.Description = value.Description;
                await db.SaveChangesAsync();
                return new ResponseData()
                {
                    Success = true,
                    Message = $"{value.CategoryName} isimli kategori başarı ile güncellenmiştir."
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        [HttpDelete]
        public async Task<ResponseData> Delete(int id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var category = db.Categories.Find(id);
                if (category == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Kategori Bulunamadı."
                    };
                }

                if (category.Products.Any())
                    throw new Exception("Kategoriye bağlı ürünler olduğu için silinemiyor.");
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return new ResponseData()
                {
                    Success = true,
                    Message = $"{category.CategoryName} isimli kategori başarı ile silinmiştir."
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
