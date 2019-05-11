using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class PHIEUDATHANG_NCC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEUDATHANG_NCC()
        {
            CT_PHIEUDATNCC = new HashSet<CT_PHIEUDATNCC>();
            PHIEUNHAP_NCC = new HashSet<PHIEUNHAP_NCC>();
        }

        [Key]
        public int MAPHIEUDAT { get; set; }

        public int MANCC { get; set; }
        
        public DateTime? NGAYDAT { get; set; }

        public int TRANGTHAI { get; set; } 

        public string NGUOIDAT { get; set; }

        public decimal TONGTIEN { get; set; }

        [ForeignKey("NGUOIDAT")]
        public virtual TBL_LOGIN TBL_LOGIN { get; set; }

        [ForeignKey("MANCC")]
        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUDATNCC> CT_PHIEUDATNCC { get; set; }
        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP_NCC> PHIEUNHAP_NCC { get; set; }
    }

}