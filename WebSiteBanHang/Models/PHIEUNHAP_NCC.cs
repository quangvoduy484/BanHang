using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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


        public DateTime? NGAYNHAP { get; set; }

        public int TRANGTHAI { get; set; }
        
        public decimal TONGTIEN { get; set; }

        public string NGUOINHAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPHIEUNHAP_NCC> CTPHIEUNHAP_NCC { get; set; }

       
        [ForeignKey("MAPHIEUDAT")]
        public virtual PHIEUDATHANG_NCC PHIEUDATHANG_NCC { get; set; }


        [ForeignKey("NGUOINHAP")]
        public virtual TBL_LOGIN TBL_LOGIN { get; set; }
    }
}