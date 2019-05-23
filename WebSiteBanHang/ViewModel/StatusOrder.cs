using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class StatusOrder:DATHANG
    {
        public string Hinh { get; set; }
        public string Tensp { get; set; }
        public int Soluong { get; set; }
        public double Giaban { get; set; }
    }
}