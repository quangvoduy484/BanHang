using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class CT_PHIEUDATNCC
    {
        [Key]
        public int MACTPD { get; set; }

        public int MAPHIEUDAT { get; set; }

        public int MASANPHAM { get; set; }

        public int? SOLUONG { get; set; }
        public string NGUOIDAT { get; set; }

        public virtual TBL_LOGIN TBL_LOGIN { get; set; }
        public virtual PHIEUDATHANG_NCC PHIEUDATHANG_NCC { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}