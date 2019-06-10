using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    public class CustomerLoginMobile
    {
        [Required(ErrorMessage = "Tên không để trống:")]
        public string EmailorPhone { get; set; }

        [Required(ErrorMessage = "Pass không để trống:")]
        public string Password { get; set; }
    }
}