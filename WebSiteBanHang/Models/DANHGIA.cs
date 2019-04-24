using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class DANHGIA
  {
    [Key]
    public int Id_DanhGia { get; set; }
    public string Comment { get; set; }
    public int SoSao { get; set; }
    public DateTime? NgayDanhGia { get; set; }
    [Column(Order = 1)]
    public int Id_KhachHang { get; set; }
    [Column(Order = 2)]
    public int Id_SanPham { get; set; }
    public virtual KHACHHANG KHACHHANG { get; set; }
    public virtual SANPHAM SANPHAM { get; set; }
  }
}
