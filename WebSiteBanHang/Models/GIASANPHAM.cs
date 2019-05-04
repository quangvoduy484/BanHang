using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class GIASANPHAM
  {
    [Key]
    public int Id_GiaSanPham { get; set; }
    public DateTime? NgayLap { get; set; }
    public Double GiaBan { get; set; }
    public bool? TrangThai { get; set; }
    public int Id_SanPham { get; set; }
    public virtual SANPHAM SANPHAM { get; set; }

  }
}
