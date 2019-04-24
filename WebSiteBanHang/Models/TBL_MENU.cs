
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Models
{
    public class TBL_MENU
    {
        public int ID { get; set; }

        public string MENU_NAME { get; set; }

        public int? MENU_SEQ { get; set; }

        public int? MENU_PARENT { get; set; }

        public bool? ACTIVATE { get; set; }

        public string MENU_ICON { get; set; }

        public int? ID_ROLE { get; set; }
        [ForeignKey("ID_ROLE")]
        public virtual TBL_ROLE TBL_ROLE { get; set; }
    }
}