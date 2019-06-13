using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Controllers
{

    public class InfoCustomerController : Controller
    {
        ThongTinKhachHangService khachHangService = new ThongTinKhachHangService();
        BanHangContext context = new BanHangContext();
        UserController userControl = new UserController();

        // GET: InfoCustomer
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int? id)
        {

            var khachHangs = khachHangService.Details();
            if (khachHangs == null)
            {
                return HttpNotFound();
            }
            return View(khachHangs);
        }
        // POST: InfoCustomer/UpdateInfoCustomer/5
        [HttpPost]
        public ActionResult UpdateInfoCustomer(Customer collection)
        {
            try
            {
                // TODO: Add update logic here
                var ketqua = khachHangService.Update(collection);
                if (!ketqua)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index", "Homepage");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Có lỗi trong quá trình xử lý");
                return View(collection);
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult ChangePassword(CustomerLogin KhachHang)
        {

            try
            {
                // TODO: Add update logic here
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    var currentUser = WebSiteBanHang.Helper.SessionUser.GetSession();

                    var kh = context.KHACHHANGs.Where(t => t.Id_KhachHang == currentUser.Id).FirstOrDefault();
                    var oldpassword = Helper.GenHash.GenSHA1(KhachHang.OldPassword);

                    if (kh.PassWord != oldpassword)
                    {
                        ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng");
                    }


                    if (KhachHang.NewPassword != KhachHang.ConfirmNewPassword)
                    {
                        ModelState.AddModelError("ConfirmNewPassword", "Mật khẩu xác nhận không đúng");
                    }
                    if(ModelState.Count == 0)
                    {
                        kh.PassWord = Helper.GenHash.GenSHA1(KhachHang.NewPassword);
                        context.SaveChanges();

                        Session["user"] = null;
                        Session["productCarts"] = null;
                        return RedirectToAction("Index", "Homepage");
                    }


                }
                return View(KhachHang);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Có lỗi trong quá trình xử lý");
                return View(KhachHang);
            }
        }
    }
}
