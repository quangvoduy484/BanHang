using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class KhachHangViewModel
    {
        public int MaKH { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgSinh { get; set; }
        public string DC { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string MK { get; set; }
        public double? DiemTichLuy { get; set; }
        public double? TongChi { get; set; }
        public int LoaiKH { get; set; }
        public bool? TrangThai { get; set; }
        public string TenLoaiKH { get; set; }
    }
}