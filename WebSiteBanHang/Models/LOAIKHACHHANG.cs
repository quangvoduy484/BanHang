using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class LOAIKHACHHANG
  {
    [Key]
    public int Id_LoaiKhachHang { get; set; }
    public string TenKhachHang { get; set; }

    public ICollection<KHACHHANG> KHACHHANGs { get; set; }
  }
}
