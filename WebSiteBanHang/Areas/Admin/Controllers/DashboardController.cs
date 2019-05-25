using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
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
            var result1 = dardboardService.GetDoanhThu();
            return View(result1);

        }

        public void XuatExcel(DateTime tuNgay, DateTime denNgay)
        {
            var rs = dardboardService.ExportExcel(tuNgay, denNgay);
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; 
            Response.AddHeader("content-disposition", "attachment:filename=Report.xlsx");
            Response.BinaryWrite(rs.GetAsByteArray());
            Response.End();
        }
    }
}