using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class CT_PhieuDatHangNCCService
    {
        private BanHangContext context = null;
        public CT_PhieuDatHangNCCService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.CT_PHIEUDATNCCs.AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.PHIEUDATHANG_NCC.GHICHU.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "MaDatHang":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.MAPHIEUDAT)
                            : model.OrderBy(t => t.MAPHIEUDAT);
                    break;
                case "TenNCC":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.SANPHAM.TenSanPham)
                            : model.OrderBy(t => t.SANPHAM.TenSanPham);
                    break;
                case "NguoiDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGUOIDAT)
                            : model.OrderBy(t => t.NGUOIDAT);
                    break;
                case "GhiChu":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.PHIEUDATHANG_NCC.GHICHU)
                            : model.OrderBy(t => t.PHIEUDATHANG_NCC.GHICHU);
                    break;
                default:
                    model = model.OrderBy(t => t.MAPHIEUDAT);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new CT_PhieuDatHangNCCViewModel()
            {

                MaPhieuDat = t.MAPHIEUDAT,
                MaSP=t.MASANPHAM,
                SL=t.SOLUONG,
                TenNguoiDat=t.NGUOIDAT,
                NgayDat=t.PHIEUDATHANG_NCC.NGAYDAT,
                GhiChu=t.PHIEUDATHANG_NCC.GHICHU,

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


