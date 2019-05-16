﻿using System;
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
        }
        //etc
        public const int DiemNhan = 100000; //100 000 = 1đ
        public const int DiemQuyDoi = 1000; // 1đ = 1000


    }
}