using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class LOAISANPHAM
  {
    [Key]
    public int Id_LoaiSanPham { get; set; }
    public string TenLoai { get; set; }
    public int Id_LoaiPhong { get; set; }
    public bool? TrangThai { get; set; }
    [ForeignKey("Id_LoaiPhong")]
    public virtual LOAIPHONG LOAIPHONG { get; set; }
    public ICollection<SANPHAM> SANPHAMs { get; set; }
  }
}
