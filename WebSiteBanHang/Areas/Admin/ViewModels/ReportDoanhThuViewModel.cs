using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class ReportDoanhThuViewModel
    {
        public ReportDoanhThuViewModel()
        {
            Label = new List<string>();
            Data = new List<double>();
        }
        public List<string> Label;
        public List<double> Data;
    }
}