using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class KHACHHANG
  {
    [Key]
    public int Id_KhachHang { get; set; }
    public string TenKhachHang { get; set; }
    public string DiaChi { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string SoDienThoai { get; set; }
    public string Email { get; set; }
    public string PassWord { get; set; }
    public double? DiemTichLuy { get; set; }
    public double? TongChi { get; set; }
    public bool? TrangThai { get; set; }
    public int Id_LoaiKhachHang { get; set; }
    public virtual LOAIKHACHHANG LOAIKHACHHANG { get; set; }
    public ICollection<DATHANG> DATHANGs { get; set; }
    public ICollection<DANHGIA> DANHGIAs { get; set; }
    public ICollection<HOADONGIAOHANG> HOADONGIAOHANGs { get; set; }

  }
}
