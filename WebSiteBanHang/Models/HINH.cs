using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
  public class HINH
  {
    [Key]
    public int Id_Hinh { get; set; }
    public string Link { get; set; }
    public int Id_SanPham { get; set; }
    public virtual SANPHAM SANPHAM { get; set; }
  }
}
