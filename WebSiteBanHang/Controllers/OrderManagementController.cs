using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;
using System.Data.Entity;
using WebSiteBanHang.Areas.Admin.Helpers;

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
            if (user == null)
            {
                return Json(new { message = "Bạn vẫn chưa có đơn đặt hàng", status = false }, JsonRequestBehavior.AllowGet);
            }
            var orderUser = db.DATHANGs
                .Include(t => t.CHITIETDATHANGs)
                .Where(x => x.Id_KhachHang == user.Id);
            if (status == 0) //or 4
                orderUser = orderUser.Where(t => t.TrangThai == 0 || t.TrangThai == 4);
            else
            {
                orderUser = orderUser.Where(t => t.TrangThai == status);
            }
            var listorder = orderUser.Select(x => new
            {
                Id_DatHang = x.Id_DatHang,
                NgayDat = x.NgayDat,
                TongTien = x.TongTien,
                order = x.CHITIETDATHANGs.Select(y => new
                {
                    Hinh = y.SANPHAM.HinhAnh ?? y.SANPHAM.HINHs.FirstOrDefault().Link,
                    Tensp = y.SANPHAM.TenSanPham,
                    Soluong = y.SoLuong,
                    ThanhTien=y.ThanhTien
                }).ToList()

            }).ToList();
            ViewBag.TongSo = listorder.Count;

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
        public ActionResult GetOderByStateByMobile(int? status ,int? idCustomer)
        {

            if (status == null || idCustomer == null)
            {
                return Json(new { message = "Bạn vẫn chưa có đơn đặt hàng", status = false }, JsonRequestBehavior.AllowGet);
            }
            var orderUser = db.DATHANGs
                .Include(t => t.CHITIETDATHANGs)
                .Where(x => x.Id_KhachHang == idCustomer);
            if (status == 0) //or 4
                orderUser = orderUser.Where(t => t.TrangThai == 0 || t.TrangThai == 4);
            else
            {
                orderUser = orderUser.Where(t => t.TrangThai == status);
            }
            var listorder = orderUser.Select(x => new
            {
                Id_DatHang = x.Id_DatHang,
                NgayDat = x.NgayDat,
                TongTien = x.TongTien,
                order = x.CHITIETDATHANGs.Select(y => new
                {
                    Hinh = y.SANPHAM.HinhAnh ?? y.SANPHAM.HINHs.FirstOrDefault().Link,
                    Tensp = y.SANPHAM.TenSanPham,
                    Soluong = y.SoLuong,
                    ThanhTien = y.ThanhTien
                }).ToList()

            }).ToList();

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

                scroreAccumulated = Math.Ceiling(double.Parse(db.KHACHHANGs.Where(x => x.Id_KhachHang == customer.Id)
                                                .Select(x => x.DiemTichLuy).FirstOrDefault().ToString()));
                scroreAccumulated = scroreAccumulated * Constant.DiemQuyDoi;

                if (sumMoney == null && sumMoney != 0)
                {
                    return Json(new { status = true, scroreAccumulated }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (SessionUser.GetSession() != null)
                    {
                        var lastTotal = sumMoney - scroreAccumulated;
                        if (lastTotal < 0)
                        {
                            lastTotal = 0;
                        }
                        return Json(new { status = true, lastTotal }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}