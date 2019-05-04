using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        var sanphams = db.SANPHAMs.Where(x => x.TrangThai == 1).OrderByDescending(x => x.CREATED_DATE).Select(x => new
        {
          Id = x.Id_SanPham,
          Ten = x.TenSanPham,
          Hinh = "/Content/Template/bower_components/ckeditor/samples/img/"+ x.HinhAnh

        }).Take(6).ToList();
        return Json(sanphams, JsonRequestBehavior.AllowGet);
      }
      catch(Exception ex)
      {
        Console.Write(ex.Message);
      }
      return null;
        

    }

  }
}