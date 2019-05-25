using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhanNhomNVViewModel
    {
        public PhanNhomNVViewModel()
        {
            NhanViens = new List<string>();
            NhanVienDropdowns = new List<NhanVienDropDownViewModel>();

        }
        public int MaNhom { get; set;}
        [Required(ErrorMessage ="Bạn chưa nhập Username")]
        
        public string TenNV { get; set;}
        public string TenNhom { get; set; }
        public string PassWord { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone(ErrorMessage ="Số điện thoại phải là 10 số")]

        public string SDT { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime Create_date { get; set;}
        public string Create_by { get; set; }
        public List<TBL_GROUP> TBL_GROUPs { get; set;}
        public List<TBL_LOGIN> TBL_LOGINs { get; set; }
        public List<string> NhanViens { get; set; }
        
        public List<NhanVienDropDownViewModel> NhanVienDropdowns { get; set; }
    }
}