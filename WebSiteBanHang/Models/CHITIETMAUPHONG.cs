using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class CHITIETMAUPHONG
    {
        [Key]
        public int Id_ChiTietMauPhong { get; set; }
        public int Id_SanPham { get; set; }
        [ForeignKey("Id_SanPham")]
        public virtual SANPHAM SANPHAM { get; set; }

        public int Id_MauPhong { get; set; }
        [ForeignKey("Id_MauPhong")]
        public virtual MAUPHONG MAUPHONG { get; set; }

    }
}