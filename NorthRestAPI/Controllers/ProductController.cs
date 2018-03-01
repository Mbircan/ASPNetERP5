using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NorthRestAPI.Models;
using NorthRestAPI.Models.ViewModels;

namespace NorthRestAPI.Controllers
{
    [RoutePrefix("api/urun")]
    public class ProductController : ApiController
    {
        [Route("")]
        public ResponseData GetAll()
        {
            try
            {
                var db = new MyNorthwindEntities();
                var urunler = db.Products.Select(x => new
                {
                    x.ProductID,
                    x.ProductName,
                    x.Category.CategoryName,
                    x.UnitPrice,
                    x.UnitsInStock,
                    TotalOrders=x.Order_Details.Count
                }).ToList();
                return new ResponseData()
                {
                    Success = true,
                    Data = urunler
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = "Ürünler getirilemedi."+ex.Message
                }; ;
            }
        }
        [Route("{cid:int:min(1)}")]
        public ResponseData GetAllByCatId(int cid)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var urunler = db.Products.Where(x => x.CategoryID == cid).Select(x => new
                {
                    x.ProductID,
                    x.ProductName,
                    x.Category.CategoryName,
                    x.UnitPrice,
                    x.UnitsInStock,
                    TotalOrders = x.Order_Details.Count
                }).ToList();
                return new ResponseData()
                {
                    Success = true,
                    Data = urunler
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = "Ürünler getirilemedi." + ex.Message
                }; ;
            }
        }
        [Route("getir/{id:int:min(1)}")]      
        public async Task<ResponseData> GetById(int id)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var urun = await db.Products.FindAsync(id);
                if (urun == null)
                {
                    return new ResponseData()
                    {
                        Message = "Ürün Bulunamadı",
                        Success = false
                    };
                }
                return new ResponseData()
                {
                    Success = true,
                    Data = new
                    {
                        urun.ProductID,
                        urun.ProductName,
                        urun.Category.CategoryName,
                        urun.UnitPrice,
                        urun.UnitsInStock
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
        [Route("ekle")]
        public async Task<ResponseData> Insert([FromBody]ProductViewModel value)
        {
            try
            {
                var db = new MyNorthwindEntities();
                db.Products.Add(new Product()
                {
                    ProductName = value.ProductName,
                    CategoryID = value.CategoryID,
                    UnitPrice = value.UnitPrice,
                    UnitsInStock = value.UnitsInStock
                });
                await db.SaveChangesAsync();
                return new ResponseData()
                {
                    Success = true,
                    Message = $"{value.ProductName} isimli ürün başarı ile eklenmiştir."
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
        [Route("guncelle")]
        public async Task<ResponseData> Update(int id, [FromBody] ProductViewModel value)
        {
            try
            {
                var db = new MyNorthwindEntities();
                var urun = await db.Products.FindAsync(id);
                if (urun == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Ürün Bulunamadı."
                    };
                }
                urun.ProductName = value.ProductName;
                urun.CategoryID = value.CategoryID;
                urun.UnitsInStock = value.UnitsInStock;
                urun.UnitPrice = value.UnitPrice;
                await db.SaveChangesAsync();
                return new ResponseData()
                {
                    Success = true,
                    Message = $"{value.ProductName} isimli ürün başarı ile güncellenmiştir."
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
        [Route("sil")]
        public async Task<ResponseData> Delete(int id)
        {
            var db=new MyNorthwindEntities();
            var urun = await db.Products.FindAsync(id);
            if (urun == null)
            {
                return new ResponseData()
                {
                    Success = false,
                    Message = "Ürün Bulunamadı."
                };
            }

            if (urun.Order_Details.Any())
                throw new Exception("Ürüne bağlı siparişler olduğu için silinemiyor.");
            db.Products.Remove(urun);
            await db.SaveChangesAsync();
            return new ResponseData()
            {
                Success = true,
                Message = $"{urun.ProductName} isimli kategori başarı ile silinmiştir."
            };
        }

    }
}
