using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class ResponseUploadImageModel
    {
        public DataReponse Data { get; set; }
        public HttpStatusCode Status { get; set; }
    }
    public  class  DataReponse
    {
        public string Link { get; set; }
    }
}