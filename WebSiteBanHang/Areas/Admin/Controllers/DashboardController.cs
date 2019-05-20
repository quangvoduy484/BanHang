using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Services;

namespace WebSiteBanHang.Areas.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        private DardboardService dardboardService = new DardboardService();
        public ActionResult Index()
        {
            var result = dardboardService.GetDoanhThu();
            return View(result);
        }
    }
}