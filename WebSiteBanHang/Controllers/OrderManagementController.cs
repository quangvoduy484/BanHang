using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;
using System.Data.Entity;
namespace WebSiteBanHang.Controllers
{
    public class OrderManagementController : Controller
    {

        BanHangContext db = new BanHangContext();
        // GET: OrderManagement

        public ActionResult createViewOrder()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getOderByState(int? status)
        {

            var user = SessionUser.GetSession();
            var listorder = db.DATHANGs
                .Include(t => t.CHITIETDATHANGs)
                .Where(x => x.TrangThai == status && x.Id_KhachHang == user.Id)
                .Select(x => new
                {
                    Id_DatHang = x.Id_DatHang,
                    NgayDat = x.NgayDat,
                    order = x.CHITIETDATHANGs.Select(y => new
                    {
                        Hinh = y.SANPHAM.HinhAnh ?? y.SANPHAM.HINHs.FirstOrDefault().Link,
                        Tensp = y.SANPHAM.TenSanPham,
                        Soluong = y.SoLuong
                    }).ToList()

                }).ToList();
            ViewBag.TongSo = listorder.Count();

            if (listorder.Count > 0)
            {
                return Json(new { orders = listorder, status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Bạn vẫn chưa có đơn đặt hàng", status = false }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult getAccumulated(bool statusAccumulated, double? sumMoney)
        {
            if (statusAccumulated)
            {
                double scroreAccumulated = 0;
                var customer = SessionUser.GetSession();

                scroreAccumulated = Math.Ceiling (double.Parse(db.KHACHHANGs.Where(x => x.Id_KhachHang == customer.Id)
                                                .Select(x => x.DiemTichLuy).FirstOrDefault().ToString()));               
                if (sumMoney == null && sumMoney !=0)
                {
                    return Json(new { status = true, scroreAccumulated = scroreAccumulated },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (SessionUser.GetSession() != null)
                    {
                        return Json(new { status = true, lastTotal = sumMoney - scroreAccumulated }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}