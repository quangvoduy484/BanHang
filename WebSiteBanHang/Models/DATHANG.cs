using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class DATHANG
    {
        [Key]
        public int Id_DatHang { get; set; }
        public DateTime? NgayDat { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string GhiChu { get; set; }
        public double TongTien { get; set; }
        public int TrangThai { get; set; }
        [Required]
        public string DiaChiGiao { get; set; }
        public string TenNguoiNhan { get; set; }   
        [Required]
        public string SoDienThoai { get; set; }
        public int Id_KhachHang { get; set; }
        public double? TongTienSauGiamGia { get; set; }
        public double? DiemTichLuy { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }

        public ICollection<CHITIETDATHANG> CHITIETDATHANGs { get; set; }
        public ICollection<HOADONGIAOHANG> HOADONGIAOHANGs { get; set; }
    }
}
