using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhieuDatHang_NCCViewModel
    {
       
        public int MaPhieuDat { get; set;}
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public DateTime? NgayDat { get; set; }
        public string NguoiDat { get; set; }
        public decimal TongTien { get; set;}
        public string TrangThai { get; set; }
        [Required]
        public List<CT_PhieuDatHangNCCViewModel> ChiTietPhieuDats { get; set; }
       
        
    }
}