using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [StringLength(50)]
        public string GHICHU { get; set; }

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEUDATNCC> CT_PHIEUDATNCC { get; set; }

        public virtual NHACUNGCAP NHACUNGCAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP_NCC> PHIEUNHAP_NCC { get; set; }
    }

}