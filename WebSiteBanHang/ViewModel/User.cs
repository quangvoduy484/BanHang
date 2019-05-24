using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    [NotMapped]
    public class User
    {
        public int Id;
        public string Name;

    }
}