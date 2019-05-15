using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class PhanNhomNVService
    {
        private BanHangContext context = null;
        public PhanNhomNVService()
        {
            context = new BanHangContext();

        }
        public object GetAll(DataTableAjaxPostModel dataModel, string id)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.TBL_GROUP_LOGINs.Where(t => t.USERNAME == id).AsQueryable();


            //serch
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    model = model.Where(t => t.TBL_GROUP_LOGINs.username.Contains(search));
            //}
            var totalRecord = model.Count();
            ////Sorting
            switch (sortBy)
            {
                case "UserName":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.USERNAME)
                            : model.OrderBy(t => t.USERNAME);
                    break;
                case "TenNhom":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TBL_GROUP.GROUPNAME)
                            : model.OrderBy(t => t.TBL_GROUP.GROUPNAME);
                    break;

                default:
                    model = model.OrderBy(t => t.TBL_LOGIN.USERNAME);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new PhanNhomNVViewModel()
            {
                MaNhom = t.ID_GROUP,
                TenNhom = t.TBL_GROUP.GROUPNAME,
                Active = t.ACTIVATE,

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