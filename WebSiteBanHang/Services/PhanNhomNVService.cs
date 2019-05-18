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