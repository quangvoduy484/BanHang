using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class NhomQuyenService
    {
        private BanHangContext context = null;
        public NhomQuyenService()
        {
            context = new BanHangContext();

        }

        public object GetAll(DataTableAjaxPostModel dataModel, int idgroup)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.TBL_GROUP_ROLEs.Where(t => t.ACTIVATE != false && t.ID_GROUP == idgroup).AsQueryable(); //lấy sản phẩm (chưa thực thi)

            //serch
            if (!string.IsNullOrWhiteSpace(search))
            {
                model = model.Where(t => t.TBL_GROUP.GROUPNAME.Contains(search));
            }
            var totalRecord = model.Count();
            //Sorting
            switch (sortBy)
            {
                case "HoTen":
                    model = dirBy == "desc" ? model.OrderByDescending(t => t.TBL_GROUP.GROUPNAME)
                            : model.OrderBy(t => t.TBL_GROUP.GROUPNAME);
                    break;

                default:
                    model = model.OrderBy(t => t.TBL_GROUP.GROUPNAME);
                    break;
            };

            //paging

            if (dataModel.length == 0) dataModel.length = 10;
            model = model.Skip(dataModel.start).Take(dataModel.length);
            var data = model.Select(t => new PhanQuyenNVViewModel()
            {
                ID_GROUP = t.ID_GROUP,
                ID_ROLE = t.ID_ROLE,
                GroupName = t.TBL_GROUP.GROUPNAME,
                RoleName = t.TBL_ROLE.ROLE_NAME,

            }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        public bool Add( int id,PhanQuyenNVViewModel model)
        {
            if (model.Quyens?.Count > 0)
            {
                foreach (var idrole in model.Quyens)
                {
                   
                        var grouprole = new TBL_GROUP_ROLE
                        {
                            ID_GROUP = id,
                            ID_ROLE = idrole,
                            ACTIVATE = true
                        };
                        context.TBL_GROUP_ROLEs.Add(grouprole);
                }
            }

            context.SaveChanges();
            return true;
        }

        public void AddAccount(PhanNhomNVViewModel model)
        {
            var newemp = new TBL_LOGIN
            {
                USERNAME = model.TenNV,
                PHONE = model.SDT,
                EMAIL = model.Email,
                PASSWORD = model.PassWord,
                ISADMIN = false,
                ACTIVATE = true,
                CREATED_DATE = DateTime.Now,
                CREATED_BY = HttpContext.Current.User.Identity.Name
            };

            context.TBL_LOGINs.Add(newemp);
            context.SaveChanges();
        }

        public bool Delete(int maNhom, int maQuyen)
        {
            TBL_GROUP_ROLE PQNVExist = context.TBL_GROUP_ROLEs.Where(t => t.ID_GROUP == maNhom
            && t.ID_ROLE == maQuyen).FirstOrDefault();
            if (PQNVExist == null)
            {
                return false;
            }
            context.TBL_GROUP_ROLEs.Remove(PQNVExist);
            context.SaveChanges();
            return true;
        }
    }
}
