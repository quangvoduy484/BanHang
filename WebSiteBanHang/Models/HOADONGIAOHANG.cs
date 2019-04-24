using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class HOADONGIAOHANG
  {
    [Key]
    public int Id_HOADONGIAOHANG { get; set; }
    public DateTime? NgayGiao { get; set; }
    public int? TrangThai { get; set; }
    public double TongTien { get; set; }
    [Column(Order = 1)]
    public int Id_DatHang { get; set; }
    [Column(Order = 2)]
    public int Id_KhachHang { get; set; }

    public virtual DATHANG DATHANG { get; set; }
    public virtual KHACHHANG KHACHHANG { get; set; }
    public ICollection<CHITIETPHIEUGIAO> CHITIETPHIEUGIAOs { get; set; }
    
  }
}
