using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;
using System.Data.Entity;
namespace WebSiteBanHang.Services
{
    public class RoomKindService
    {
        private BanHangContext context = null;
        public RoomKindService()
        {
            context = new BanHangContext();

        }
        public RoomKindViewModel GetRoomKind(int maPhong, int? maLoaiSP)
        {

            var result = context.LOAIPHONGs
                .Include(t => t.LOAISANPHAMs)
                .Include(t => t.LOAISANPHAMs.Select(m => m.SANPHAMs))
                .Include(t => t.LOAISANPHAMs.Select(m => m.SANPHAMs.Select(n => n.KHUYENMAI)))
                .Include(t => t.LOAISANPHAMs.Select(m => m.SANPHAMs.Select(n => n.GIASANPHAMs)))
                .Include(t => t.LOAISANPHAMs.Select(m => m.SANPHAMs.Select(n => n.HINHs)))
                .Where(t => t.Id_LoaiPhong == maPhong && t.TrangThai != false)
                .Select(t => new RoomKindViewModel()
                {
                    MaPhong = t.Id_LoaiPhong,
                    TenPhong = t.TenLoaiPhong,
                    MoTa = t.MoTa,
                    LoaiSanPhams = t.LOAISANPHAMs.Where(x => x.TrangThai != false)
                    .Select(x => new LoaiSPViewModel()
                    {
                        MaLoai = x.Id_LoaiSanPham,
                        TenLoai = x.TenLoai
                    }).ToList(),
                    // selectmany dùng cho trường hợp đi qua từng loại sản phẩm lấy được list sản phẩm 
                    SanPhams = t.LOAISANPHAMs
                        .Where(k => !maLoaiSP.HasValue || k.Id_LoaiSanPham == maLoaiSP.Value && k.TrangThai != false)
                        .SelectMany(x => x.SANPHAMs)
                        .Where(k => k.TrangThai != false)
                        .OrderByDescending(z => z.CREATED_DATE)
                        .Select(y => new SanPhamModel()
                        {
                            MaSanPham = y.Id_SanPham,
                            TenSanPham = y.TenSanPham,
                            HinhAnh = y.HinhAnh ?? y.HINHs.FirstOrDefault().Link,
                            KhuyenMai = y.KHUYENMAI.NgayBatDau <= DateTime.Now && DateTime.Now <= y.KHUYENMAI.NgayKetThuc ?
                                         (int?)y.KHUYENMAI.GiaTriKhuyenMai : null,
                            GiaGoc = y.GIASANPHAMs.Where(x => x.TrangThai != false).OrderByDescending(m => m.NgayLap).FirstOrDefault().GiaBan,

                        })
                        .Take(12)
                        .ToList()
                }).FirstOrDefault();

            return result;

            //var loaiPhong = context.LOAIPHONGs.Find(maPhong);
            //if (loaiPhong == null) return null;

            //var abc = (from p in context.LOAIPHONGs
            //           join l in context.LOAISANPHAMs.AsQueryable() on p.Id_LoaiPhong equals l.Id_LoaiPhong
            //           join sp in context.SANPHAMs on l.Id_LoaiSanPham equals sp.Id_LoaiSanPham

            //           join g in context.GIASANPHAMs on sp.Id_SanPham equals g.Id_SanPham into gi
            //           from x in gi.DefaultIfEmpty()

            //           join h in context.HINHs on sp.Id_SanPham equals h.Id_SanPham into hi
            //           from n in hi.DefaultIfEmpty()

            //           join km in context.KHUYENMAIs on sp.Id_KhuyenMai equals km.Id_KhuyenMai into khm
            //           from t in khm.DefaultIfEmpty()

            //           where p.Id_LoaiPhong == maPhong && sp.TrangThai != false && x.TrangThai != false
            //            && t.NgayBatDau <= DateTime.Now && DateTime.Now <= t.NgayKetThuc
            //           orderby x.NgayLap descending

            //           select new RoomKindViewModel()
            //           {
            //               MaPhong = p.Id_LoaiPhong,
            //               MoTa = p.MoTa,
            //               TenPhong = p.TenLoaiPhong,
            //               LoaiSanPhams = 
            //               SanPhams = new List<SanPhamModel>()
            //              {
            //                  new SanPhamModel()
            //                  {
            //                       MaSanPham = sp.Id_SanPham,
            //                        TenSanPham = sp.TenSanPham,
            //                        HinhAnh = sp.HinhAnh ?? n.Link,
            //                        GiaGoc = gi.FirstOrDefault().GiaBan,
            //                        KhuyenMai = (int?) t.GiaTriKhuyenMai,
            //                        GiaBan = t.GiaTriKhuyenMai > 0 ?  gi.FirstOrDefault().GiaBan * t.GiaTriKhuyenMai :  gi.FirstOrDefault().GiaBan
            //                  }
            //              }

            //           }).FirstOrDefault();


        }
    }
}