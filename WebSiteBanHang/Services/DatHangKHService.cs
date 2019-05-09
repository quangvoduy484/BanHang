using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using static WebSiteBanHang.Areas.Admin.Helpers.Constant;

namespace WebSiteBanHang.Services
{
    public class DatHangKHService
    {
        private BanHangContext context = null;
        public DatHangKHService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.DATHANGs.Where(t => t.TrangThai != 0).AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.KHACHHANG.TenKhachHang.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "NgayDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayDat)
                            : model.OrderBy(t => t.NgayDat);
                    break;
                case "TongTien":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TongTien)
                            : model.OrderBy(t => t.TongTien);
                    break;
                case "TenKhachHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.KHACHHANG.TenKhachHang)
                            : model.OrderBy(t => t.KHACHHANG.TenKhachHang);
                    break;
                default:
                    model = model.OrderBy(t => t.NgayDat);
                    break;
            };
            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model
                .Include(t => t.KHACHHANG)
                .AsEnumerable()
                .Select(t => new DatHangKHViewModel()
                {
                    MaDatHang = t.Id_DatHang,
                    NgayDatHang = t.NgayDat,
                    GhiChu = t.GhiChu,
                    TongTien = t.TongTien,
                    TrangThai = ConvertToTrangThaiModel(t.TrangThai),
                    TenKhachHang = t.KHACHHANG.TenKhachHang,
                }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        public List<ChiTietDatHangViewModel> GetChiTietDatHangs(int id)
        {
            var chiTiets = context.CHITIETDATHANGs.Where(t => t.Id_DatHang == id && t.TrangThai != false &&
                    t.DATHANG.TrangThai != 0)
                .Select(t => new ChiTietDatHangViewModel()
                {
                    TenSanPham = t.SANPHAM.TenSanPham,
                    SoLuong = t.SoLuong,
                    GiaBan = t.GiaBan,
                    ThanhTien = t.ThanhTien,
                }).ToList();

            return chiTiets;

        }

        private string ConvertToTrangThaiModel(int trangThai)
        {
            if (trangThai == 0)
            {
                return TinhTrangDatHang.DaHuy;
            }
            if (trangThai == 1)
            {
                return TinhTrangDatHang.DangXyLy;
            }
            return TinhTrangDatHang.DaGiao;
        }
        private int ConvertToTrangThaiInt(string trangThai)
        {
            if (trangThai == TinhTrangDatHang.DaHuy)
            {
                return 0;
            }
            if (trangThai == TinhTrangDatHang.DangXyLy)
            {
                return 1;
            }
            return 2;
        }
        //public List<DATHANG> ListAll()
        //{
        //    return context.DATHANGs.OrderBy(t => t.NgayDat).ToList();
        //}
        public List<KHACHHANG> GetAllKH()
        {
            return context.KHACHHANGs.ToList();
        }
        public void Add(DatHangKHViewModel model)
        {
            var dathang = new DATHANG
            {
                Id_DatHang = model.MaDatHang,
                Id_KhachHang = model.MaKhachHang,
                NgayDat = model.NgayDatHang,
                GhiChu = model.GhiChu,
                TongTien = model.TongTien,
                TrangThai = ConvertToTrangThaiInt(model.TrangThai)
            };
            context.DATHANGs.Add(dathang);
            context.SaveChanges();
        }
    }
}