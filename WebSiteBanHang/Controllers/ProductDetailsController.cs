using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ProductDetailsController : Controller
    {
        BanHangContext db = new BanHangContext();
        // GET: ProductDetails
        public ActionResult getDetailProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = db.SANPHAMs.Include(x => x.HINHs).Where(x => x.TrangThai == 1 && x.Id_SanPham == id).SingleOrDefault(); 
            if(product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}