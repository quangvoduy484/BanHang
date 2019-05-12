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
        [Required(ErrorMessage = "Tên không để trống:")]
        public string TenKhachHang { get; set; }

     
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không để trống")]
        [Phone(ErrorMessage = "Không được nhập chứ")]
        public string SoDienThoai { get; set; }

        [Required(ErrorMessage = "Emal không để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu  không để trống")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiếu 6 kí tự")]
        public string PassWord { get; set; }

        public int? Ngay { get; set; }
        public int? Thang { get; set; }
        public int? Nam { get; set; }
    }
}