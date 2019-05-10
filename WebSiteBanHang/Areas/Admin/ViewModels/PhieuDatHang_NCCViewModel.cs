using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class PhieuDatHang_NCCViewModel
    {
       
        public int MaPhieuDat { get; set; }
        public int MaCTPhieuDat { get; set;}
        public int MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string GhiChu { get; set; }
        public string NguoiDat { get; set; }
        public DateTime? NgayDat { get; set;}
        public int? SL { get; set;}
        public int MaSP { get; set; }
        public string TenSP { get; set; }


    }
}