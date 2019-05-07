using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class NhaCungCapViewModel
    {
       
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string SDT { get; set; }
        public string DC { get; set; }
        public bool TrangThai { get; set; }

    }
}