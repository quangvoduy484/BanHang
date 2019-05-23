using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;

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

        public JsonResult getOderByState(int? status = 1)
        {

            var user = SessionUser.GetSession();
            var listorder = db.DATHANGs.Join(db.CHITIETDATHANGs, dh => dh.Id_DatHang, ct => ct.Id_DatHang, (dh, ct) => new { DATHANGs = dh, CHITIETDATHANG = ct })
                                   .Join(db.SANPHAMs, x => x.CHITIETDATHANG.Id_SanPham, sp => sp.Id_SanPham, (x, sp) => new { x.DATHANGs, x.CHITIETDATHANG, SANPHAM = sp })
                                   .Where(x => x.DATHANGs.TrangThai == status && x.DATHANGs.Id_KhachHang == user.Id)
                                   .Select(x => new StatusOrder()
                                   {
                                       Id_DatHang = x.DATHANGs.Id_DatHang,
                                       NgayDat = x.DATHANGs.NgayDat,
                                       Tensp = x.SANPHAM.TenSanPham,
                                       Hinh = x.SANPHAM.HinhAnh,
                                       Soluong = x.CHITIETDATHANG.SoLuong,
                                       Giaban = x.CHITIETDATHANG.GiaBan

                                   }).AsQueryable().ToList();
            ViewBag.TongSo = listorder.Count();

            if (listorder.Count > 0)
            {
                return Json(new { orders = listorder }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { message = "Bạn vẫn chưa có đơn đặt hàng"}, JsonRequestBehavior.AllowGet);
            }
        
        }
    }
}