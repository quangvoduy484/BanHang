using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class Customer
    {
       
        public string TenKhachHang { get; set; }
        public string DiaChi { get; set; }
        public int MaKhachHang { get; set; }
        [MinLength(10, ErrorMessage = "Số điện thoại không đủ kí tự")]
        [Phone(ErrorMessage = "Không được nhập chữ")]
        public string SoDienThoai { get; set; }

      
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

       
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiếu 6 kí tự")]
        public string PassWord { get; set; }

        public int? Ngay { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
        public int LoaiKH { get; set; }
        public string TenLoaiKH { get; set; }
        public double? DiemTichLuy { get; set; }
        public double? TongChi { get; set; }
        public DateTime? NgSinh { get; set; }
        
    }
}