using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.Services;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Controllers
{
    public class InfoCustomerController : Controller
    {
        ThongTinKhachHangService khachHangService = new ThongTinKhachHangService();
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


    }
}
