using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class KIEUPHONG
  {
    [Key]
    public int Id_KieuPhong { get; set; }
    public string TenKieuPhong { get; set; }
    public string HinhAnh { get; set; }
    public bool? TrangThai { get; set; }

    public int Id_LoaiPhong { get; set; }
    public virtual LOAIPHONG LOAIPHONG { get; set; }

    public ICollection<LOAISANPHAM> LOAISANPHAMs { get; set; }


  }
}
