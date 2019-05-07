using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class CTPHIEUNHAP_NCC
    {
        [Key]
        public int MACTPN { get; set; }

        public int MAPHIEUNHAP { get; set; }

        public int MASANPHAM { get; set; }

        public int? SOLUONGNHAP { get; set; }

        public decimal? GIANHAP { get; set; }

        public decimal? THANHTIEN { get; set; }

        public string NGUOINHAP { get; set; }
        public virtual TBL_LOGIN TBL_LOGIN { get; set; }

        public virtual PHIEUNHAP_NCC PHIEUNHAP_NCC { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}