using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhieuNhapHang_NCCViewModel
    {
        public int MaPhieuDat { get; set; }
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public DateTime? NgayDat { get; set; }
        public string NguoiDat { get; set; }
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }
        
        public List<CT_PhieuNhapHangNCCViewModel> cT_PhieuNhapHangNCCs { get; set;}
    }
}