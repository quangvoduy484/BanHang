using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class CT_PhieuDatHangNCCViewModel
    {
        public int MaPhieuDat { get; set; }
        public int MaCTPN { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int? SL { get; set; }
        public string TenNguoiDat { get; set; }
        public string TenNCC { get; set; }
        public DateTime? NgayDat { get; set; }
        public string GhiChu { get; set; }
    }
}