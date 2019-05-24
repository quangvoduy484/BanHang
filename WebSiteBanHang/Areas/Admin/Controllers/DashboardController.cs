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

        public ActionResult XuatExcel(DateTime tuNgay, DateTime denNgay)
        {
            var rs = dardboardService.ExportExcel(tuNgay, denNgay);
            Response.ContentType = "application/vnd.openxmlfomats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment:filename=" + "ExcelReport.xlsx");
            var abyte = rs.GetAsByteArray();
            return File(abyte, "application/pdf");
        }
    }
}