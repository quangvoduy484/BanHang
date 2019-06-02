using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class SuccessBillService
    {
        private BanHangContext context = null;
        public SuccessBillService()
        {
            context = new BanHangContext();

        }

        public DatHangKHViewModel GetThongTinDatHang(int id_KH)
        {

            var address = context.DIACHIs.Where(x => x.Id_KhachHang == id_KH && x.TrangThai == true).SingleOrDefault();
            var datHang = context.DATHANGs
                  .Where(t => t.Id_KhachHang== id_KH)
                  .Select(t => new DatHangKHViewModel()
                  {
                      //Thông tin đơn hàng
                      MaDatHang = t.Id_DatHang,
                      NgayDatHang=t.NgayDat,
                      TongTien = t.TongTien,

                      //Thông tin khách hàng
                      DiaChiGiao = t.DiaChiGiao,
                      SoDienThoai = t.SoDienThoai,
                      TenKhachHang = address.TenKhachHang,
                      TenKhachHangDat=t.KHACHHANG.TenKhachHang,
                      SoDienThoaiDat=t.KHACHHANG.SoDienThoai,
                      // Thông tin chi tiết đơn đặt hàng
                      ChiTietDatHangs = t.CHITIETDATHANGs.Where(k => k.TrangThai != false)
                      .Select(k => new ChiTietDatHangViewModel()
                      {
                          TenSanPham = k.SANPHAM.TenSanPham,
                          MaChiTiet = k.Id_ChiTietDatHang,
                          SoLuong = k.SoLuong,
                          GiaBan = k.GiaBan,
                          ThanhTien = k.SoLuong * k.GiaBan,
                      }).ToList(),
                  })
                  .FirstOrDefault();
            if (datHang == null) return null;
            return datHang;

        }
    }
}