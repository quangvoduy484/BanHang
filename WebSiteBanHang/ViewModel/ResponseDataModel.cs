using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.ViewModel
{
    public class ResponseDataModel
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public CustomerInformation CustomerInfo { get; set; }

    }
}