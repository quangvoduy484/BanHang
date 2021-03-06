﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSiteBanHang.Areas.Admin.ViewModels
{
    public class SanPhamViewModel
    {
        public SanPhamViewModel()
        {
            HinhAnhs = new List<HinhAnhViewModel>();
        }
        [Key]
        public int MaSanPham { get; set; }
        [Required]
        public string TenSanPham { get; set; }
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
        public double? GiaSP { get; set; }
        public int? MaKM { get; set; }
        public string TenKhuyenMai { get; set; }
        public double GiaTriKhuyenMai { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string XuatXu { get; set; }
        public int? SoLuongTon { get; set; }
        public string DVT { get; set; }

        public string MauSac { get; set; }

        public string VatLieu { get; set; }

        public string KichThuoc { get; set; }
        public string HinhAnh { get; set; }
        public string BaoHanh { get; set; }
        public string MoTa { get; set; }
        public List<HinhAnhViewModel> HinhAnhs { get; set; }
    }
}