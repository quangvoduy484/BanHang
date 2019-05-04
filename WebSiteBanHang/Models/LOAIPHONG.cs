using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class LOAIPHONG
  {
    [Key]
    public int Id_LoaiPhong { get; set; }
    public string TenLoaiPhong { get; set; }
    public string HinhAnh { get; set; }
    public string MoTa { get; set; }
    public string TenNoiThat { get; set; }
    public bool? TrangThai { get; set; }
    public ICollection<MAUPHONG> MAUPHONGs { get; set; }
    public ICollection<LOAISANPHAM> LOAISANPHAMs { get; set; }

    }
}
