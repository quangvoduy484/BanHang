using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using System.Data.Entity;

namespace WebSiteBanHang.Services
{
    public class PhanNhomNVService
    {
        private BanHangContext context = null;
        public PhanNhomNVService()
        {
            context = new BanHangContext();

        }
        // Lấy danh sách nhóm 
        public List<TBL_GROUP> getNhom()
        {
            return context.TBL_GROUPs.Where(t => t.ACTIVATE == true).ToList().Select(t => new TBL_GROUP()
            {
                ID=t.ID,
                GROUPNAME=t.GROUPNAME,

            }).ToList();
        }
        public object GetAll(DataTableAjaxPostModel dataModel)
        {
            var sortBy = dataModel.columns[dataModel.order[0].column].data; //Lấy cột để sắp xếp
            var dirBy = dataModel.order[0].dir.ToLower(); //Lấy thứ tự tăng/giảm
            var search = dataModel.search.value;

            var model = context.TBL_GROUP_LOGINs.Where(t => t.ACTIVATE != false ).AsQueryable(); //lấy sản phẩm (chưa thực thi)

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

                MaNhom=t.ID_GROUP,
                TenNhom=t.TBL_GROUP.GROUPNAME,
                TenNV=t.USERNAME,
                Email=t.TBL_LOGIN.EMAIL,
                PassWord=t.TBL_LOGIN.PASSWORD,
                SDT=t.TBL_LOGIN.PHONE,
            }).ToList();

            return new
            {
                draw = Convert.ToInt32(dataModel.draw),
                recordsTotal = totalRecord,
                recordsFiltered = totalRecord,
                data = data
            };
        }

        //Lấy danh sách nhân viên theo nhóm
        public List<TBL_LOGIN> GetNV(int idgroup)
        {
            return context.TBL_LOGINs.Include(t =>t.TBL_GROUP_LOGINs)
                .Where(t=>t.ACTIVATE !=false)
                .Where(t=>t.TBL_GROUP_LOGINs.All(x=>x.ID_GROUP!= idgroup && x.ACTIVATE!=false ) ).ToList();
        }

        //Thêm nhân viên mới vào nhóm
        public void Add(TBL_GROUP_LOGIN model,int idgroup)
        {
            var phannhom = new TBL_GROUP_LOGIN
            {
                ID_GROUP = idgroup,
                USERNAME = model.USERNAME,
                ACTIVATE = true,
            };
            context.TBL_GROUP_LOGINs.Add(phannhom);
            context.SaveChanges();
        }
        // Thêm nhóm mới
        public void AddGroup(TBL_GROUP model)
        {
            var nhom = new TBL_GROUP
            {
                ID=model.ID,
                GROUPNAME=model.GROUPNAME,
            };
            context.TBL_GROUPs.Add(nhom);
            context.SaveChanges();
        }
    }
}