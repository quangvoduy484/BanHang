using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class TBL_GROUP
    {

        public int ID { get; set; }
        public string GROUPNAME { get; set; }
        public string GROUPDESC { get; set; }
        public bool ACTIVATE { get; set; }


        [StringLength(20)]
        public string CREATED_BY { get; set; }

        public DateTime? CREATED_DATE { get; set; }

        [StringLength(20)]
        public string UPDATED_BY { get; set; }

        public DateTime? UPDATED_DATE { get; set; }

        public virtual ICollection<TBL_GROUP_LOGIN> TBL_GROUP_LOGINs { get; set; }

        public virtual ICollection<TBL_GROUP_ROLE> TBL_GROUP_ROLEs { get; set; }


    }
}