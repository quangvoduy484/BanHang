using System;
using System.Collections.Generic;
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

            result.Label.AddRange(abc.Select(t => t.Label));
            result.Data.AddRange(abc.Select(t => t.Data));

            return result;
        }
    }
}