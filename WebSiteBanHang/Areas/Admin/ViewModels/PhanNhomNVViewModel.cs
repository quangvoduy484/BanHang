using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhanNhomNVViewModel
    {
        public int MaNhom { get; set;}
        public string TenNV { get; set;}
        public string TenNhom { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public bool Active { get; set; }
        public List<TBL_GROUP> TBL_GROUPs { get; set;}
        public List<TBL_LOGIN> TBL_LOGINs { get; set; }
    }
}