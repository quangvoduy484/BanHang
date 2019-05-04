using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class ReponseMessage
    {
        public string Message { get; set; }
        public HttpStatusCode StatusCode { get; set;     }
    }
}