using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class ProductDetail : SANPHAM
    {
        public ProductDetail()
        {
            Hinhs = new List<string>();
        }
        public double GiaBan { get; set; }
        public List<string> Hinhs { get; set; }
    }
}