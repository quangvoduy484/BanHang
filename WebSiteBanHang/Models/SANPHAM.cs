using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class SANPHAM
    {
        [Key]
        public int Id_SanPham { get; set; }
        public string TenSanPham { get; set; }
        public string DonViTinh { get; set; }
        public string MauSac { get; set; }
        public string VatLieu { get; set; }
        public string KichThuoc { get; set; }
        public string HinhAnh { get; set; }
        public string Mota { get; set; }
        public bool? TrangThai { get; set; }
        //public DateTime NgayBatDauBaoHanh { get; set; }
        public string BaoHanh { get; set; }
        public int? SoLuongTon { get; set; }
        public string XuatXu { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }

        [Column(Order = 1)]
        public int Id_LoaiSanPham { get; set; }
        [Column(Order = 2)]
        public int Id_KhuyenMai { get; set; }

        public virtual KHUYENMAI KHUYENMAI { get; set; }
        public virtual LOAISANPHAM LOAISANPHAM { get; set; }
        public ICollection<GIASANPHAM> GIASANPHAMs { get; set; }
        public ICollection<CHITIETDATHANG> CHITIETDATHANGs { get; set; }
        public ICollection<DANHGIA> DANHGIAs { get; set; }
        public ICollection<CHITIETPHIEUGIAO> CHITIETPHIEUGIAOs { get; set; }
        public ICollection<HINH> HINHs { get; set; }
        public ICollection<CHITIETMAUPHONG> CHITIETMAUPHONGs { get; set; }
    }
}
