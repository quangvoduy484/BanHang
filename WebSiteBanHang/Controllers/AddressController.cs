using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class AddressController : Controller
    {
        BanHangContext db = new BanHangContext();
        // GET: Address
        [HttpGet]
        public JsonResult getAdressUserLogin()
        {
            if (SessionUser.GetSession() != null)
            {
                var user = SessionUser.GetSession();
                var customer = db.KHACHHANGs.Where(x => x.Id_KhachHang == user.Id).SingleOrDefault();

                var addressUser = db.DIACHIs.AsEnumerable().Where(x => x.Id_KhachHang == customer.Id_KhachHang && x.TrangThai == true)
                                 .Select(x => new DIACHI()
                                 {
                                     Id_KhachHang = x.Id_KhachHang,
                                     TenKhachHang = x.TenKhachHang,
                                     DiaChi = x.DiaChi,
                                     SoDienThoai = x.SoDienThoai,
                                 }).AsQueryable().SingleOrDefault();
                return Json(new { adrressUser = addressUser, login = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { login = false }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult PartialAdress()
        {
            var customer = SessionUser.GetSession();
            ViewBag.Ten = customer.Name;
            var listAdress = db.DIACHIs.Where(x => x.Id_KhachHang == customer.Id).ToList();
            return PartialView(listAdress);
        }

        [HttpGet]
        public ActionResult addAdressPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult addAdressPartial(DIACHI address)
        {
            if (SessionUser.GetSession() != null)
            {
                var anewAddress = new DIACHI()
                {
                    Id_KhachHang = SessionUser.GetSession().Id,
                    TenKhachHang = address.TenKhachHang,
                    DiaChi = address.DiaChi,
                    SoDienThoai = address.SoDienThoai,
                };
                db.DIACHIs.Add(anewAddress);
                db.SaveChanges();
                return RedirectToAction("renderProductCarts","Cart");
            }
            return PartialView();
        }

        public JsonResult changeAddress(int IdAddress, int IdCustomer)
        {
            var address = db.DIACHIs.Where(x => x.Id_KhachHang == IdCustomer).ToList();
            if(IdCustomer !=0)
            {
                foreach (var item in address)
                {
                    if (item.Id_DiaChi == IdAddress)
                    {
                        item.TrangThai = true;
                    }
                    else
                    {
                        item.TrangThai = false;
                    }
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                }
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }
    }
}