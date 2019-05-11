using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class CHITIETDATHANG
    {
        [Key]
        public int Id_ChiTietDatHang { get; set; }

        public int SoLuong { get; set; }
        public double GiaBan { get; set; }
        public double ThanhTien { get; set; }
        [Column(Order = 1)]
        public int Id_DatHang { get; set; }
        [Column(Order = 2)]
        public int Id_SanPham { get; set; }

        public bool? TrangThai { get; set; }
        public virtual DATHANG DATHANG { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
