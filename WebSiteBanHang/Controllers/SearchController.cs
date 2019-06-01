using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Controllers
{
    public class SearchController : Controller
    {
        SanPhamService sanPhamService = new SanPhamService();
        // GET: Search
        public ActionResult Index(string keysearch)
        {
            if(string.IsNullOrWhiteSpace(keysearch))
            {
                return View();
            }
            var sanPhams = sanPhamService.GetSanPhamsByKeySearch(keysearch);
            return View(sanPhams);
        }

    }
}
