using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class DIACHI
    {
        [Key]
        public int Id_DiaChi { get; set; }
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public bool TrangThai { get; set; }

        public int Id_KhachHang { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}