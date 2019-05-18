using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class NhanVienService
    {
        BanHangContext context = new BanHangContext();
        public NhanVienService()
        {
             context = new BanHangContext();
        }
        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.TBL_GROUP_LOGINs.Where(t => t.ACTIVATE != false).AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.USERNAME.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "HoTen":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.USERNAME)
                            : model.OrderBy(t => t.USERNAME);
                    break;

                default:
                    model = model.OrderBy(t => t.USERNAME);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new PhanNhomNVViewModel()
            {

                MaNhom = t.ID_GROUP,
                TenNhom = t.TBL_GROUP.GROUPNAME,
                TenNV = t.USERNAME,
                Email = t.TBL_LOGIN.EMAIL,
                PassWord = t.TBL_LOGIN.PASSWORD,
                SDT = t.TBL_LOGIN.PHONE,
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