using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class DatHangKHViewModel
    {
        [Key]
        public int MaDatHang { get; set; }
        public int MaKhachHang { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string TenKhachHang { get; set; }
        public DateTime? NgayDatHang { get; set; }
        [Required]
        [StringLength(225)]
        public string DiaChiGiao { get; set; }
        [StringLength(10)]
        [MinLength(10)]
        [Required]
        public string SoDienThoai { get; set; }
        public double TongTien { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public List<ChiTietDatHangViewModel> ChiTietDatHangs { get; set; }
    }
}