using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                case "MaCTPD":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.MACTPD)
                            : model.OrderBy(t => t.MACTPD);
                    break;
                case "MaSP":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.MASANPHAM)
                            : model.OrderBy(t => t.MASANPHAM);
                    break;
                case "TenNguoiDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGUOIDAT)
                            : model.OrderBy(t => t.NGUOIDAT);
                    break;
                //case "TenNCC":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.NHACUNGCAP.TENNCC)
                //            : model.OrderBy(t => t.NHACUNGCAP.TENNCC);
                //    break;
                //case "NgayDat":
                //    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGAYDAT)
                //            : model.OrderBy(t => t.NGAYDAT);
                //    break;

                default:
                    model = model.OrderBy(t => t.MAPHIEUDAT);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model
                .Include(t => t.SANPHAM)
                .AsEnumerable()
                .Select(t => new CT_PhieuDatHangNCCViewModel()
            {
                MaPhieuDat = t.MAPHIEUDAT,
               MaCTPD=t.MACTPD,
               TenSP=t.SANPHAM.TenSanPham,
               SL =t.SOLUONG,
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


