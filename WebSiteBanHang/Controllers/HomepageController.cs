using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class HomepageController : Controller
    {
        BanHangContext db = new BanHangContext();
        // GET: Homepage
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getProductNew()
        {
            try
            {
                var sanphams = db.SANPHAMs.Include(x => x.HINHs).Where(x => x.TrangThai != false && x.GIASANPHAMs.Any(y => y.Id_SanPham == x.Id_SanPham)).OrderByDescending(x => x.CREATED_DATE).Select(x => new
                {
                    Id = x.Id_SanPham,
                    Ten = x.TenSanPham,
                    Hinh = x.HinhAnh ?? x.HINHs.FirstOrDefault().Link,
                    // phần trăm có thể có hay không nên có thể để kiểu int ?
                    PhanTram = x.KHUYENMAI.NgayBatDau <= DateTime.Now && DateTime.Now <= x.KHUYENMAI.NgayKetThuc  ?
                                         (int?)x.KHUYENMAI.GiaTriKhuyenMai : null,
                    // sau mà cần lấy giá trị một cột chỉ cần Fistordefault 
                    GiaGoc = x.GIASANPHAMs.Where(y => y.TrangThai != false).OrderByDescending(m => m.NgayLap).FirstOrDefault().GiaBan


                }).Take(6).ToList();
                return Json(sanphams, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return null;


        }


        public JsonResult getNameLogin()
        {

            if (SessionUser.GetSession() != null)
            {
                return Json(new { hadlogin = "true", name = SessionUser.GetSession().Name }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { hadlogin = "false" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getProductMost()
        {
            var products = db.SANPHAMs.Join(db.CHITIETDATHANGs, sp => sp.Id_SanPham, ctdh => ctdh.Id_SanPham, (sp, ctdh) 
                           => new { sanpham=sp,chitietdh = ctdh }).GroupBy(x => new { x.chitietdh.Id_SanPham})
                             .Select(g => new {
                                        IdSanPham = g.Key.Id_SanPham,
                                        TenSanPham =  db.SANPHAMs.Where(s => s.Id_SanPham == g.Key.Id_SanPham).FirstOrDefault().TenSanPham,
                                        HinhAnh = db.SANPHAMs.Where(s => s.Id_SanPham == g.Key.Id_SanPham).FirstOrDefault().HinhAnh ?? db.SANPHAMs.Where(s => s.Id_SanPham == g.Key.Id_SanPham).FirstOrDefault().HINHs.FirstOrDefault().Link,
                                        SoLuong = g.Sum(sum => sum.chitietdh.SoLuong)
                             }).OrderByDescending(g => g.SoLuong).Take(9).ToList().AsQueryable();

            return Json(new { products }, JsonRequestBehavior.AllowGet);


        }
    }
}
