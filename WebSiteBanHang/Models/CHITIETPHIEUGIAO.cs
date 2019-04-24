using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class CHITIETPHIEUGIAO
  {
    [Key]
    public int Id_ChiTietPhieuGiao { get; set; }
    public int SoLuongGiao { get; set; }
    public double GiaBan { get; set; }
    public double ThanhTien { get; set; }

    [Column(Order = 1)]
    public int Id_HOADONGIAOHANG { get; set; }
    [Column(Order = 2)]
    public int Id_SanPham { get; set; }
    public virtual HOADONGIAOHANG HOADONGIAOHANG { get; set; }
    public virtual SANPHAM SANPHAM { get; set; }
  }
}
