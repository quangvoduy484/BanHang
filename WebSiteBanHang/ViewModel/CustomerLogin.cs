using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class CustomerLogin
    {
        [Required(ErrorMessage = "Tên không để trống:")]
        public string EmailorPhone { get; set; }

        [Required(ErrorMessage = "Pass không để trống:")]
        public string Password { get; set; }

        [Display(Name = "Mật khẩu cũ:")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ")]
        [Compare(otherProperty: "Password", ErrorMessage = "Mật khẩu cũ không đúng")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Mật khẩu mới:")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
        [MinLength(6, ErrorMessage = "Mật khẩu tối thiếu 6 kí tự")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Nhập lại mật khẩu:")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu xác nhận")]
        [Compare(otherProperty: "NewPassword", ErrorMessage = "Không trùng nhau")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }


    }

    [NotMapped]
    public class CustomerLoginMobile
    {
        [Required(ErrorMessage = "Tên không để trống:")]
        public string EmailorPhone { get; set; }

        [Required(ErrorMessage = "Pass không để trống:")]
        public string Password { get; set; }
    }
}