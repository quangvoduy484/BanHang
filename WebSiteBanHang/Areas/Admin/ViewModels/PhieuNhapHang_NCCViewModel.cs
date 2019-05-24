using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhieuNhapHang_NCCViewModel
    {
        public int MaPhieuDat { get; set; }
      
        public decimal TongTien { get; set; }
        public string TrangThai { get; set; }

        public string NguoiNhap { get; set;}
        public DateTime? NgayNhap { get; set; }
        
        public List<CT_PhieuNhapHangNCCViewModel> ChiTietPhieuNhaps { get; set;}
        public List<PhieuDatHang_NCCViewModel> ChiTietPhieuDats { get; set; }
    }
}