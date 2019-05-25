using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
//using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                    NgayGiao = t.Key.NgayGiao.Value,
                    TongDonHang = t.Count(),
                    TongTien = t.Sum(x => x.TongTien),
                })
                .ToList();


            ExcelPackage pck = new ExcelPackage();

            ExcelWorksheet ews = pck.Workbook.Worksheets.Add("Report");
            ExcelRange range = ews.Cells["A1:A3"];
            range.Merge = true;
            var fileName = HttpContext.Current.Server.MapPath("/Content/Images/nha-xinh-logo.jpg");
            var picture = ews.Drawings.AddPicture("abc", new FileInfo(fileName));
            picture.SetSize(120, 200);
            picture.From.Column = 0;
            picture.From.Row = 0;
            picture.To.Column = 1;
            picture.To.Row = 3;

            ews.Column(1).Width = 20;

            ews.Cells["B1"].Value = "NHÀ XINH FUNITURE";
            ews.Cells["B1"].Style.Font.Color.SetColor(Color.Red);
            ews.Cells["B1"].Style.Font.Size = 14;

            ews.Cells["C1"].Value = "Ngày thống kê";
            ews.Cells["D1"].Value = string.Format("{0:dd/MM/yyyy} - {0:H: mm tt}", DateTimeOffset.Now);
            ews.Cells["D1"].Style.Font.Italic = true;


            ews.Cells["C2"].Value = "Người thống kê";
            ews.Cells["D2"].Value = HttpContext.Current.User.Identity.Name;
            ews.Cells["D2"].Style.Font.Italic = true;

            ews.Cells["B2"].Value = "140 Lê Trọng Tấn, Quận Tân Phú";
            ews.Cells["B2"].Style.Font.Size = 14;
            ews.Cells["B2"].Style.Font.Color.SetColor(Color.Red);

            ews.Cells["B3"].Value = "0767073048";
            ews.Cells["B3"].Style.Font.Size = 14;
            ews.Cells["B3"].Style.Font.Color.SetColor(Color.Red);

            ews.Cells["A6"].Value = "STT";
            ews.Cells["A6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ews.Cells["A6"].Style.Font.Bold = true;

            ews.Cells["B6"].Value = "Ngày Giao";
            ews.Cells["B6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ews.Cells["B6"].Style.Font.Bold = true;

            ews.Cells["C6"].Value = "Tổng đơn hàng";
            ews.Cells["C6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ews.Cells["C6"].Style.Font.Bold = true;

            ews.Cells["D6"].Value = "Tổng tiền";
            ews.Cells["D6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            ews.Cells["D6"].Style.Font.Bold = true;
            int serialNumber = 1;
            int rowStart = 7;

            var tongDT = abc.Sum(t => t.TongTien);
            foreach (var chiTiet in abc)
            {
                if (serialNumber % 2 == 0)
                {
                    var cel = ews.Cells[string.Format("A{0}:D{0}", rowStart)];
                    cel.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    cel.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
                }
                ews.Cells[String.Format("A{0}", rowStart)].Value = serialNumber++.ToString();
                ews.Cells[String.Format("B{0}", rowStart)].Value = chiTiet.NgayGiao.ToString("dd/MM/yyyy");
                ews.Cells[String.Format("C{0}", rowStart)].Value = chiTiet.TongDonHang;
                ews.Cells[String.Format("D{0}", rowStart)].Value = chiTiet.TongTien;
                //
                ews.Cells[String.Format("A{0}", rowStart)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ews.Cells[String.Format("B{0}", rowStart)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ews.Cells[String.Format("C{0}", rowStart)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ews.Cells[String.Format("D{0}", rowStart)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);


                rowStart++;
            }
            ews.Cells[String.Format("C{0}", rowStart)].Value = "Tổng doanh thu";
            ews.Cells[String.Format("D{0}", rowStart)].Value = tongDT;
            ews.Cells["A:AZ"].AutoFitColumns(20);
            return pck;

        }
    }

}