using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class MenuRole : TBL_MENU
    {
        public string RouterLink { get; set; }
    }
}