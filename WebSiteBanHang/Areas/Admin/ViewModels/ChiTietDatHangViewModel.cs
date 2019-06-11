using System.ComponentModel.DataAnnotations;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class ChiTietDatHangViewModel
    {
        [Key]
        public int MaChiTiet { get; set; }
        public string TenSanPham { get; set; }
        public string BaoHanh { get; set; }
        public int SoLuong { get; set; }
        public double GiaBan { get; set; }
        public double ThanhTien { get; set; }
    }
}