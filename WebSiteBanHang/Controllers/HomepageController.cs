using System;
using System.Collections.Generic;
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
                var sanphams = db.SANPHAMs.Where(x => x.TrangThai != false).OrderByDescending(x => x.CREATED_DATE).Select(x => new
                {
                    Id = x.Id_SanPham,
                    Ten = x.TenSanPham,
                    Hinh = x.HinhAnh

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
    }
}
