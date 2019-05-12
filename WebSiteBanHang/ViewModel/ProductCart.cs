using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class ProductCart:SANPHAM
    {
        public int soluong { get; set; }
        public int chon { get; set; }
        public double giagoc { get; set; }
        public string loaisp { get; set; }
        public double phantramgiamgia { get; set; }
        public double giagiam { get; set; }
    }
}