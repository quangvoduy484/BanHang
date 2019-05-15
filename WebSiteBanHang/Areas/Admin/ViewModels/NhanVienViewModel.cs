using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class NhanVienViewModel
    {
        [Required]
        public string UserName { get; set;}
        public string PassWord { get; set;}
        public string Email { get; set;}
        public string SDT { get; set;}

    }
}