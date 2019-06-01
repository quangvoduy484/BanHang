using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    //[Table("NHACUNGCAP")]
    public partial class NHACUNGCAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACUNGCAP()
        {
            PHIEUDATHANG_NCC = new HashSet<PHIEUDATHANG_NCC>();
        }

        [Key]
        public int MANCC { get; set; }

        [StringLength(100)]
        [Required]
        public string TENNCC { get; set; }

        [StringLength(100)]
        public string DIACHI { get; set; }

        [StringLength(10)]
        [Phone]
       
        public string SODIENTHOAI { get; set; }

        public bool? TRANGTHAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDATHANG_NCC> PHIEUDATHANG_NCC { get; set; }
    }
}
