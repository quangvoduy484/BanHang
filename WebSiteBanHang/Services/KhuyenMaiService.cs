using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using System.Data.Entity;
namespace WebSiteBanHang.Services
{
    public class KhuyenMaiService
    {
        private BanHangContext context = null;
        public KhuyenMaiService()
        {
            context = new BanHangContext();

        }
        public List<KHUYENMAI> ListAll()
        {
            return context.KHUYENMAIs.ToList().Select(t => new KHUYENMAI()
            {
                Id_KhuyenMai = t.Id_KhuyenMai,
                TenKhuyenMai = t.TenKhuyenMai,
                NgayBatDau = t.NgayBatDau,
                NgayKetThuc = t.NgayKetThuc,
                GiaTriKhuyenMai = t.GiaTriKhuyenMai
            }).ToList();
        }
        public void Add(KhuyenMaiViewModel model)
        {
            var khuyenMai = new KHUYENMAI()
            {
                TenKhuyenMai = model.TenKhuyenMai,
                GiaTriKhuyenMai = model.GiaTriKhuyenMai,
                NgayBatDau = model.NgayBatDau,
                NgayKetThuc = model.NgayKetThuc,
            };

            context.KHUYENMAIs.Add(khuyenMai);
            context.SaveChanges();

            if (model.SanPhams?.Count > 0)
            {
                foreach (var item in model.SanPhams)
                {
                    var sanPham = context.SANPHAMs.FirstOrDefault(t => t.Id_SanPham == item && t.TrangThai != false);
                    if (sanPham == null) continue;
                    sanPham.Id_KhuyenMai = khuyenMai.Id_KhuyenMai;
                }
            }
            context.SaveChanges();
        }
        public bool Update(KHUYENMAI khuyenMai)
        {

            KHUYENMAI khuyenMaiExist = context.KHUYENMAIs.Find(khuyenMai.Id_KhuyenMai);
            if (khuyenMaiExist == null)
            {
                return false;
            }
            khuyenMaiExist.TenKhuyenMai = khuyenMai.TenKhuyenMai;
            khuyenMaiExist.NgayBatDau = khuyenMai.NgayBatDau;
            khuyenMaiExist.NgayKetThuc = khuyenMai.NgayKetThuc;
            khuyenMaiExist.GiaTriKhuyenMai = khuyenMai.GiaTriKhuyenMai;
            context.SaveChanges();
            return true;
        }

        public KhuyenMaiViewModel FindById(int? id)
        {
            var km = context.KHUYENMAIs
                 .Include(t => t.SANPHAMs)
                 .Where(t => t.Id_KhuyenMai == id)
                 .Select(t => new KhuyenMaiViewModel()
                 {
                     TenKhuyenMai = t.TenKhuyenMai,
                     GiaTriKhuyenMai = t.GiaTriKhuyenMai,
                     NgayKetThuc = t.NgayKetThuc,
                     NgayBatDau = t.NgayBatDau,
                     Id_KhuyenMai = t.Id_KhuyenMai,
                     SanPhamDropdowns = t.SANPHAMs.Select(x => new SanPhamDropDownViewModel()
                     {
                         id = x.Id_SanPham,
                         text = x.TenSanPham
                     }).ToList()
                 }).FirstOrDefault();
            return km;
        }
        public bool Delete(int maKM)
        {
            KHUYENMAI khuyenMaiExist = context.KHUYENMAIs.Find(maKM);
            if (khuyenMaiExist == null)
            {
                return false;
            }
            context.KHUYENMAIs.Remove(khuyenMaiExist);
            context.SaveChanges();
            return true;
        }
    }
}
