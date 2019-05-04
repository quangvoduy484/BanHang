using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class LoaiPhongService
    {
        private BanHangContext context = null;
        public LoaiPhongService()
        {
            context = new BanHangContext();

        }
        public List<LOAIPHONG> ListAll()
        {
            return context.LOAIPHONGs.ToList().Select(t => new LOAIPHONG()
            {
                Id_LoaiPhong = t.Id_LoaiPhong,
                TenLoaiPhong = t.TenLoaiPhong,
                HinhAnh = t.HinhAnh
            }).ToList();
        }
        public void Add(LOAIPHONG loaiPhong)
        {
            context.LOAIPHONGs.Add(loaiPhong);
            context.SaveChanges();
        }
        public bool Update(LOAIPHONG loaiPhong)
        {

            LOAIPHONG loaiPhongExist = context.LOAIPHONGs.Find(loaiPhong.Id_LoaiPhong);
            if (loaiPhongExist == null)
            {
                return false;
            }
            loaiPhongExist.HinhAnh = loaiPhong.HinhAnh;
            loaiPhongExist.TenLoaiPhong = loaiPhong.TenLoaiPhong;
            context.SaveChanges();
            return true;
        }

        public LOAIPHONG FindById(int? id)
        {
            return context.LOAIPHONGs.Find(id);
        }
        public bool Delete(int maPhong)
        {
            LOAIPHONG loaiPhongExist = context.LOAIPHONGs.Find(maPhong);
            if (loaiPhongExist == null)
            {
                return false;
            }
            context.LOAIPHONGs.Remove(loaiPhongExist);
            context.SaveChanges();
            return true;
        }
    }
}
