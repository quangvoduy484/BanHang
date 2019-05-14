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

            var model = context.DATHANGs.AsQueryable(); //lấy sản phẩm (chưa thực thi)
            //Where(t => t.TrangThai != 0).
            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.KHACHHANG.TenKhachHang.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "Id_DatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.Id_DatHang)
                            : model.OrderBy(t => t.Id_DatHang);
                    break;
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
                case "NgayGiao":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NgayGiao)
                            : model.OrderBy(t => t.NgayGiao);
                    break;
                default:
                    model = model.OrderBy(t => t.Id_DatHang);
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
                    NgayGiao = t.NgayGiao,
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

        public DatHangKHViewModel GetChiTietDatHangs(int id)
        {

            var datHang = context.DATHANGs
                  .Where(t => t.Id_DatHang == id)
                  .Select(t => new DatHangKHViewModel()
                  {
                      MaDatHang = t.Id_DatHang,
                      //thong tin them
                      TenKhachHang = t.KHACHHANG.TenKhachHang,
                      DiaChiGiao = t.DiaChiGiao,
                      SoDienThoai = t.SoDienThoai,
                      GhiChu = t.GhiChu,

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
                DiaChiGiao = model.DiaChiGiao,
                SoDienThoai = model.SoDienThoai,
                GhiChu = model.GhiChu,
                TongTien = model.TongTien,
                TrangThai = ConvertToTrangThaiInt(model.TrangThai)
            };
            context.DATHANGs.Add(dathang);
            context.SaveChanges();
        }
        public bool DeleteDetail(int maCTDH)
        {
            CHITIETDATHANG CTDHExit = context.CHITIETDATHANGs.FirstOrDefault(t => t.TrangThai != false && t.Id_ChiTietDatHang == maCTDH);
            if (CTDHExit == null)
            {
                return false;
            }
            CTDHExit.TrangThai = false;
            // update tong tien
            //lay dat hang

            var datHang = context.DATHANGs
                    .Include(t => t.CHITIETDATHANGs)
                    .FirstOrDefault(t => t.Id_DatHang == CTDHExit.Id_DatHang && t.TrangThai == 1);
            if (datHang == null)
            {
                return false;
            }

            //tinh tong tien

            var newTongTien = datHang.CHITIETDATHANGs.Where(t => t.TrangThai != false).
                Sum(t => t.SoLuong * t.GiaBan);

            //update tong tien

            datHang.TongTien = newTongTien;

            //save
            context.SaveChanges();
            return true;
        }
        public bool DeleteDatHangKH(int maDatHang)
        {
            DATHANG datHangExist = context.DATHANGs.FirstOrDefault(t => t.TrangThai == 1 && t.Id_DatHang == maDatHang);
            if (datHangExist == null)
            {
                return false;
            }
            datHangExist.TrangThai = 0;
            context.SaveChanges();
            return true;
        }
        public bool UpdateCTDH(ChiTietDatHangViewModel model)
        {
            CHITIETDATHANG CTDHExit = context.CHITIETDATHANGs.FirstOrDefault(t => t.TrangThai != false && t.Id_ChiTietDatHang == model.MaChiTiet);
            if (CTDHExit == null)
            {
                return false;
            }
            CTDHExit.SoLuong = model.SoLuong;
            CTDHExit.ThanhTien = model.SoLuong * CTDHExit.GiaBan;
            var datHang = context.DATHANGs
                   .Include(t => t.CHITIETDATHANGs)
                   .FirstOrDefault(t => t.Id_DatHang == CTDHExit.Id_DatHang && t.TrangThai == 1);
            if (datHang == null)
            {
                return false;
            }

            //tinh tong tien

            var newTongTien = datHang.CHITIETDATHANGs.Where(t => t.TrangThai != false).
                Sum(t => t.SoLuong * t.GiaBan);

            //update tong tien

            datHang.TongTien = newTongTien;

            //save
            context.SaveChanges();
            return true;
        }
        public bool UpdateDHKH(DatHangKHViewModel model)
        {
            DATHANG datHang = context.DATHANGs.FirstOrDefault(t => t.Id_DatHang == model.MaDatHang);
            if (datHang == null)
            {
                return false;
            }
            if (datHang.TrangThai != 1 && datHang != null)
            {
                return false;
            }
            if (datHang.TrangThai == 1 && datHang != null)
            {
                datHang.DiaChiGiao = model.DiaChiGiao;
                datHang.SoDienThoai = model.SoDienThoai;
                context.SaveChanges();
            }
            return true;

        }
        public DatHangKHViewModel findbyId(int id)
        {
            return context.DATHANGs
                .Where(t => t.TrangThai == 1 && t.Id_DatHang == id)
                .Select(t => new DatHangKHViewModel()
                {
                    MaDatHang = t.Id_DatHang,
                    DiaChiGiao = t.DiaChiGiao,
                    SoDienThoai = t.SoDienThoai,
                }).FirstOrDefault();
        }
    }
}