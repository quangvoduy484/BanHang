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
        public object GetAll(DataTableAjaxPostModel dataModel, int idgroup)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.TBL_GROUP_LOGINs.Where(t => t.ACTIVATE != false && t.ID_GROUP == idgroup).AsQueryable(); //lấy sản phẩm (chưa thực thi)

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

        
        public List<NhanVienDropDownViewModel> GetAllDropDownList(string search, int idgroup)
        {
            //var nhanViens = context.TBL_LOGINs.Where(t => t.ACTIVATE != false);
            var nhanViens = context.TBL_LOGINs
                 .Where(t => t.ACTIVATE != false)
                 .Where(t => t.TBL_GROUP_LOGINs.All(x => x.ID_GROUP != idgroup && x.ACTIVATE != false));

            if (!string.IsNullOrWhiteSpace(search))
            {
                nhanViens = nhanViens.Where(t => t.USERNAME.Contains(search));

            }
            var result = nhanViens.OrderBy(t => t.USERNAME)
                .Take(30)
                .Select(t => new NhanVienDropDownViewModel()
                {
                    id = t.USERNAME,
                    text = t.USERNAME,
                }).ToList();

            return result;
        }

        public bool Add(PhanNhomNVViewModel model)
        {
            var nv = new TBL_LOGIN
            {
                USERNAME=model.TenNV,
                PASSWORD=model.PassWord,
                EMAIL=model.Email,
                PHONE=model.SDT,
                ACTIVATE = true,
                ISADMIN = false,
                CREATED_DATE = DateTime.Now,
                CREATED_BY = HttpContext.Current.User.Identity.Name,
        };

         context.TBL_LOGINs.Add(nv);
            context.SaveChanges();
            return true;
        }

        public bool Delete(int maNhom, string tennv)
        {
            TBL_GROUP_LOGIN PNNVExist = context.TBL_GROUP_LOGINs.Where(t => t.ID_GROUP == maNhom
            && t.USERNAME.Equals(tennv, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (PNNVExist == null)
            {
                return false;
            }
            context.TBL_GROUP_LOGINs.Remove(PNNVExist);
            context.SaveChanges();
            return true;
        }

        
    }
}