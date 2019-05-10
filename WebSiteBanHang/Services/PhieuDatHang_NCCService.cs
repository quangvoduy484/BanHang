using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class PhieuDatHang_NCCService
    {
        private BanHangContext context = null;
        public PhieuDatHang_NCCService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.PHIEUDATHANG_NCCs.AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.GHICHU.Contains(search));
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
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NHACUNGCAP.TENNCC)
                            : model.OrderBy(t => t.NHACUNGCAP.TENNCC);
                    break;
                case "NgayDat":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.NGAYDAT)
                            : model.OrderBy(t => t.NGAYDAT);
                    break;
               
                default:
                    model = model.OrderBy(t => t.MAPHIEUDAT);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model
                
                .Select(t => new PhieuDatHang_NCCViewModel()
            {
                MaPhieuDat=t.MAPHIEUDAT,
                TenNCC=t.NHACUNGCAP.TENNCC,
                NgayDat=t.NGAYDAT,
                GhiChu=t.GHICHU,
               
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