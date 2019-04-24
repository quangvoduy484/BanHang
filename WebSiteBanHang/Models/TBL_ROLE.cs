using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class TBL_ROLE
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string ROLE_NAME { get; set; }

        [StringLength(500)]
        public string ROLE_DEF { get; set; }

        public string ROLE_LINK { get; set; }

        public bool ACTIVATE { get; set; }

        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string UPDATED_BY { get; set; }

        public DateTime? UPDATED_DATE { get; set; }

        public bool? Inactive { get; set; }

        public virtual ICollection<TBL_MENU> TBL_MENUs { get; set; }

        public virtual ICollection<TBL_GROUP_ROLE> TBL_GROUP_ROLEs { get; set; }


    }
}