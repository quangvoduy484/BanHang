using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class PHIEUNHAP_NCC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUNHAP_NCC()
        {
            CTPHIEUNHAP_NCC = new HashSet<CTPHIEUNHAP_NCC>();
        }

        [Key]
        public int MAPHIEUNHAP { get; set; }

        public int MAPHIEUDAT { get; set; }

        public int MANHANVIEN { get; set; }

        public DateTime? NGAYNHAP { get; set; }

        public decimal? TONGTIEN { get; set; }

        [StringLength(50)]
        public string GHICHU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPHIEUNHAP_NCC> CTPHIEUNHAP_NCC { get; set; }

       

        public virtual PHIEUDATHANG_NCC PHIEUDATHANG_NCC { get; set; }
    }
}