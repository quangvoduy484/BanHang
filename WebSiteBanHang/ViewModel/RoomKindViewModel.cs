using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;

namespace WebSiteBanHang.ViewModel
{
    public class RoomKindViewModel
    {
        public RoomKindViewModel()
        {
            SanPhams = new List<SanPhamModel>();
            LoaiSanPhams = new List<LoaiSPViewModel>();
        }
        public int MaPhong { get; set; }
        public string TenPhong { get; set; }
        public string MoTa { get; set; }
        public List<LoaiSPViewModel> LoaiSanPhams { get; set; }
        public List<SanPhamModel> SanPhams { get; set; }
    }
    public class SanPhamModel
    {
        public int MaSanPham { get; set; }
        public string TenSanPham
        {
            get; set;
        }

        public double? GiaBan
        {
            get; set;
        }
        public int? KhuyenMai { get; set; }
        public double? GiaGoc { get; set; }
        public string HinhAnh { get; set; }
    }

}