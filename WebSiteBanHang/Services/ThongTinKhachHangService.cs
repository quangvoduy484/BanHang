using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Helper;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;

namespace WebSiteBanHang.Services
{
    public class ThongTinKhachHangService
    {
        private BanHangContext context = null;
        public ThongTinKhachHangService()
        {
            context = new BanHangContext();

        }
        public Customer Details()
        {

            var currentUser = WebSiteBanHang.Helper.SessionUser.GetSession();
            if (currentUser == null)
            {
                return null;
            }
            var khachHangs = context.KHACHHANGs
                .FirstOrDefault(t => t.Id_KhachHang == currentUser.Id);
            if (khachHangs == null)
            {
                return null;
            }
            var result = new Customer()
            {
                TenKhachHang = khachHangs.TenKhachHang,
                NgSinh = khachHangs.NgaySinh,
                DiemTichLuy = khachHangs.DiemTichLuy,
                SoDienThoai = khachHangs.SoDienThoai,
                DiaChi = khachHangs.DiaChi,
                TenLoaiKH = khachHangs.LOAIKHACHHANG.TenKhachHang,
                TongChi = khachHangs.TongChi,
                Email = khachHangs.Email,
                PassWord = khachHangs.PassWord,
            };
            return result;
        }
        public bool Update(Customer KhachHang)
        {
            var currentUser = WebSiteBanHang.Helper.SessionUser.GetSession();

            if (currentUser == null)
            {
                return false;
            }

            KHACHHANG KhachHangExist = context.KHACHHANGs.Find(currentUser.Id);
            if (KhachHangExist == null)
            {
                return false;
            }
            KhachHangExist.TenKhachHang = KhachHang.TenKhachHang;
            KhachHangExist.DiaChi = KhachHang.DiaChi;
            KhachHangExist.SoDienThoai = KhachHang.SoDienThoai;
            KhachHangExist.NgaySinh = KhachHang.NgSinh;
            KhachHangExist.Email = KhachHang.Email;
            context.SaveChanges();
            return true;
        }


        public KHACHHANG FindById(int id)
        {
            var userlogin = SessionUser.GetSession();
            id = userlogin.Id;
            return context.KHACHHANGs.Find(id);
        }

       

    }
}