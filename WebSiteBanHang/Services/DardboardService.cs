using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class DardboardService
    {

        private BanHangContext context = null;
        public DardboardService()
        {
            context = new BanHangContext();

        }

        public ReportDoanhThuViewModel GetDoanhThu()
        {
            var result = new ReportDoanhThuViewModel();
            //get doanh thu trong nam hien tai
            var abc = context.DATHANGs
                .Where(t => t.TrangThai == 2 && t.NgayGiao.HasValue &&
                    t.NgayGiao.Value.Year == DateTime.Now.Year)
                .GroupBy(t => new { t.NgayGiao.Value.Month })
                .Select(t => new
                {
                    Label = "Tháng " + t.Key.Month,
                    Data = t.Sum(x => x.TongTien),
                })
                .ToList();
            var DonDH = context.DATHANGs
                .Where(t => t.TrangThai == 2 && t.NgayGiao.HasValue &&
                    t.NgayGiao.Value.Year == DateTime.Now.Year)
                    .Count();
            result.Label.AddRange(abc.Select(t => t.Label));
            result.Data.AddRange(abc.Select(t => t.Data));
            result.TongDT = result.Data.Sum();
            result.TongDH = DonDH;
            return result;
        }
        //Thoosng kee
        public ExcelPackage ExportExcel(DateTime tuNgay, DateTime denNgay)
        {
            //var result = new ReportDoanhThuViewModel();
            //get doanh thu trong nam hien tai
            var abc = context.DATHANGs
                .Where(t => t.TrangThai == 2 && t.NgayGiao.HasValue &&
                t.NgayGiao >= tuNgay && t.NgayGiao <= denNgay)
                .GroupBy(t => new { t.NgayGiao })
                .Select(t => new
                {
                    Label = "Ngày " + t.Key.NgayGiao,
                    Data = t.Sum(x => x.TongTien),
                })
                .ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ews = pck.Workbook.Worksheets.Add("Report");

            ews.Cells["A1"].Value = "Communication";
            ews.Cells["B1"].Value = "Com1";
            ews.Cells["A2"].Value = "Report";
            ews.Cells["B2"].Value = "Report1";


            ews.Cells["A6"].Value = "STT";
            ews.Cells["B6"].Value = "Label";
            ews.Cells["C6"].Value = "Data";
            int serialNumber = 1;
            int rowStart = 6;
            foreach (var chiTiet in abc)
            {
                ews.Cells[String.Format("A{0}", rowStart)].Value = serialNumber++.ToString();
                ews.Cells[String.Format("B{0}", rowStart)].Value = chiTiet.Label;
                ews.Cells[String.Format("C{0}", rowStart)].Value = chiTiet.Data;
            }
            ews.Cells["A:AZ"].AutoFitColumns();
            return pck;
      
        }
    }
    
}