using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class MAUPHONG
  {
    [Key]
    public int Id_MauPhong { get; set; }
    public string TenMauPhong { get; set; }
    public string HinhAnh { get; set; }
    public bool? TrangThai { get; set; }

    public int Id_LoaiPhong { get; set; }
    public virtual LOAIPHONG LOAIPHONG { get; set; }
    public ICollection<CHITIETMAUPHONG> CHITIETMAUPHONGs { get; set; }
  }
}
