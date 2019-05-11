using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class KhachHangService
    {
        private BanHangContext context = null;
        public KhachHangService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.KHACHHANGs.Where(t => t.TrangThai != false).AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TenKhachHang.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "HoTen":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TenKhachHang)
                            : model.OrderBy(t => t.TenKhachHang);
                    break;
                case "TenLoaiKH":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.LOAIKHACHHANG.TenKhachHang)
                            : model.OrderBy(t => t.LOAIKHACHHANG.TenKhachHang);
                    break;
                //case "TenLoai":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.LOAIKHACHHANG.TenKhachHang)
                //            : model.OrderBy(t => t.LOAIKHACHHANG.TenKhachHang);
                //    break;
                //case "DVT":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.DonViTinh)
                //            : model.OrderBy(t => t.DonViTinh);
                //    break;
                //case "KichThuoc":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.KichThuoc)
                //            : model.OrderBy(t => t.KichThuoc);
                //    break;
                default:
                    model = model.OrderBy(t => t.TenKhachHang);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new KhachHangViewModel()
            {

                MaKH=t.Id_KhachHang,
                HoTen=t.TenKhachHang,
                SDT=t.SoDienThoai,
                Email=t.Email,
                LoaiKH=t.Id_LoaiKhachHang,
               TenLoaiKH=t.LOAIKHACHHANG.TenKhachHang,
            }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        public KhachHangViewModel Details(int? id)
        {
            var KHs = context.KHACHHANGs.FirstOrDefault(t => t.TrangThai != false && t.Id_KhachHang == id);
            if (KHs == null)
            {
                return null;
            }
            var result = new KhachHangViewModel()
            {
               
                HoTen=KHs.TenKhachHang,
                NgSinh=KHs.NgaySinh,
                DC=KHs.DiaChi,
                SDT=KHs.SoDienThoai,
                Email=KHs.Email,
                MK=KHs.PassWord,
                DiemTichLuy=KHs.DiemTichLuy,
                TongChi=KHs.TongChi,
                TenLoaiKH=KHs.TenKhachHang,
            };
            return result;
        }
        public List<KHACHHANG> ListAll()
        {
            return context.KHACHHANGs.OrderBy(t => t.TenKhachHang).ToList();
        }
    }
}