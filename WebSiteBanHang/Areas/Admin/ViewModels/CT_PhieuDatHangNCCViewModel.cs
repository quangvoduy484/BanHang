using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class CT_PhieuDatHangNCCViewModel
    {
        public int MaPhieuDat { get; set; }
        public int MaCTPhieuDat { get; set; }
        public int MaSP { get; set; }
        public int MaNCC { get; set; }
        public string TenSanPham { get; set; }
        public int SL { get; set; }
        public decimal GiaNhap { get; set;}
        public decimal ThanhTien { get; set; }
        public decimal TongTien { get; set; }
        public string TenNguoiDat { get; set; }
        public DateTime? NgayDat { get; set; }
        public string TenNCC { get; set; }
        public List<SanPhamViewModel> sanPhamViewModels { get; set; }
    }
}