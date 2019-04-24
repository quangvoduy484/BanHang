using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class LOAISANPHAM
  {
    [Key]
    public int Id_LoaiSanPham { get; set; }
    public string TenLoai { get; set; }
    public int Id_KieuPhong { get; set; }
    public bool? TrangThai { get; set; }
    public virtual KIEUPHONG KIEUPHONG { get; set; }
    public ICollection<SANPHAM> SANPHAMs { get; set; }
  }
}
