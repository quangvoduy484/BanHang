using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Models;

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
                TenKhuyenMai=t.TenKhuyenMai,
                NgayBatDau = t.NgayBatDau,
                NgayKetThuc = t.NgayKetThuc,
                GiaTriKhuyenMai = t.GiaTriKhuyenMai
            }).ToList();
        }
        public void Add(KHUYENMAI khuyenMai)
        {
            context.KHUYENMAIs.Add(khuyenMai);
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

        public KHUYENMAI FindById(int? id)
        {
            return context.KHUYENMAIs.Find(id);
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
