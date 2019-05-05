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
                case "LoaiKH":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.Id_LoaiKhachHang)
                            : model.OrderBy(t => t.Id_LoaiKhachHang);
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
               // TenLoaiKH=t.LOAIKHACHHANG.TenKhachHang,
            }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }
    }
}