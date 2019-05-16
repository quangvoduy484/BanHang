using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class KhuyenMaiViewModel
    {
        public KhuyenMaiViewModel()
        {
            SanPhams = new List<int>();
            SanPhamDropdowns = new List<SanPhamDropDownViewModel>();
        }
        [Key]
        public int Id_KhuyenMai { get; set; }
        [StringLength(1000)]
        public string TenKhuyenMai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public double GiaTriKhuyenMai { get; set; }
        public List<int> SanPhams { get; set; }
        public List<SanPhamDropDownViewModel> SanPhamDropdowns { get; set; }
    }
}