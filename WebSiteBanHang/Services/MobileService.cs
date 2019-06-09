using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;
using System.Data.Entity;
namespace WebSiteBanHang.Services
{

    public class MobileService
    {
        private readonly BanHangContext context;
        public MobileService()
        {
            context = new BanHangContext();
        }

        public CustomerInformation Login(string username, string password)
        {
            var genpass = Helper.GenHash.GenSHA1(password);
            var customerInfo = context.KHACHHANGs
               .Where(x => x.PassWord == genpass && (x.Email == username || x.SoDienThoai == username))
               .Select(c => new CustomerInformation()
               {
                   Name = c.TenKhachHang,
                   Address = c.DiaChi,
                   Id = c.Id_KhachHang,
                   PhoneNunber = c.SoDienThoai,
                   Email = c.Email
               })
               .SingleOrDefault();

            return customerInfo;
        }
        public List<OrderViewModel> GetOrderByStatus(int? status, int? idCustomer)
        {
            var orderUser = context.DATHANGs
               .Include(t => t.CHITIETDATHANGs)
               .Where(x => x.Id_KhachHang == idCustomer);
            if (status == 0) //or 4
                orderUser = orderUser.Where(t => t.TrangThai == 0 || t.TrangThai == 4);
            else
            {
                orderUser = orderUser.Where(t => t.TrangThai == status);
            }
            var listorder = orderUser.Select(x => new OrderViewModel
            {
                MaDatHang = x.Id_DatHang,
                NgayDat = x.NgayDat,
                TongTien = x.TongTien,
                Products = x.CHITIETDATHANGs.Select(y => new ProductViewModel
                {
                    Hinh = y.SANPHAM.HinhAnh ?? y.SANPHAM.HINHs.FirstOrDefault().Link,
                    TenSanPham = y.SANPHAM.TenSanPham,
                    SoLuong = y.SoLuong,
                    ThanhTien = y.ThanhTien.ToString()
                }).ToList()

            }).ToList();

            return listorder;
        }
    }
}