using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhanQuyenNVViewModel
    {
        public PhanQuyenNVViewModel()
        {
            QuyenDropDowns = new List<QuyenDropDownViewModel>();
        }
        public int ID_GROUP { get; set;}
        public int ID_ROLE { get; set;}
        public string GroupName { get; set;}
        public string RoleName { get; set; }
        public bool Active { get; set; }
        public List<int> Quyens { get; set; }

        public List<QuyenDropDownViewModel> QuyenDropDowns { get; set; }
    }
}