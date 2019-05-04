using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class TBL_LOGIN
    {
        [Key]
        [StringLength(20)]
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        [StringLength(500)]
        public string EMAIL { get; set; }
        [StringLength(20)]
        public string PHONE { get; set; }
        public bool? TYPE { get; set; }
        public bool ISADMIN { get; set; }
        // thuộc tính này quyết định tài khoản có còn hoạt động hay không
        public bool ACTIVATE { get; set; }


        [StringLength(20)]
        public string CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string UPDATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }

        public bool? Inactive { get; set; }
        public virtual ICollection<TBL_GROUP_LOGIN> TBL_GROUP_LOGINs { get; set; }


    }
}
