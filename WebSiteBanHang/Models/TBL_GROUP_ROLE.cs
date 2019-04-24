using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class TBL_GROUP_ROLE
    {

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_GROUP { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_ROLE { get; set; }

        public bool? ACTIVATE { get; set; }

        public virtual TBL_GROUP TBL_GROUP { get; set; }

        public virtual TBL_ROLE TBL_ROLE { get; set; }
    }
}