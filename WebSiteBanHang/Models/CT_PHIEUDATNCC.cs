using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int SOLUONG { get; set; }

        public decimal GIANHAP { get; set; }
         
        public decimal THANHTIEN { get; set; }

        public int TRANGTHAI { get; set; }

        public virtual PHIEUDATHANG_NCC PHIEUDATHANG_NCC { get; set; }

        [ForeignKey("MASANPHAM")]
        public virtual SANPHAM SANPHAM { get; set; }
    }
}