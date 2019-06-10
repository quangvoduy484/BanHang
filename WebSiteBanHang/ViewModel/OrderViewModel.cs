using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    public class OrderViewModel
    {
        public int MaDatHang { get; set; }
        public string NgayDat { get; set; }
        public string TongTien { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }

    public class ProductViewModel
    {
        public string TenSanPham { get; set; }
        public string ThanhTien { get; set; }
        public int SoLuong { get; set; }
        public string Hinh { get; set; }
    }
}