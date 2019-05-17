using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class KHUYENMAI
    {
        [Key]
        public int Id_KhuyenMai { get; set; }
        [StringLength(1000)]
        public string TenKhuyenMai { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayBatDau { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NgayKetThuc { get; set; }
        public double GiaTriKhuyenMai { get; set; }
        public ICollection<SANPHAM> SANPHAMs { get; set; }

    }
}
