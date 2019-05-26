using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.Helpers
{
    public class Constant
    {
        public struct TinhTrangDatHang
        {
            public const string DaHuy = "Đã huỷ";
            public const string DangXyLy = "Đang xử lý";
            public const string DaGiao = "Đã giao";
            public const string DangDat = "Đang đặt hàng";//TT=2 (DH_NCC)
            public const string DaNhan = "Đã nhập"; //  TT=3 (NH_NCC)

        }
        //etc
        public const int DiemNhan = 100000; //100 000 = 1đ
        public const int DiemNhanVIP = 50000; //100 000 = 1đ
        public const int DiemQuyDoi = 1000; // 1đ = 1000
        public const int TongChiVIP=100000000; // 1đ = 1000
        public const int TongChiThanThiet = 0; // 1đ = 1000
    }
}