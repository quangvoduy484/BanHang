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
        private BanHangContext context = null;
        public NhanVienService()
        {
            context = new BanHangContext();

        }
        public List<TBL_LOGIN> ListAll()
        {
            return context.TBL_LOGINs.Where(t=>t.ACTIVATE ==true).ToList().Select(t => new TBL_LOGIN()
            {
                USERNAME = t.USERNAME,
                PASSWORD=t.PASSWORD,
                EMAIL=t.EMAIL,
                PHONE=t.PHONE,

            }).ToList();
        }
        //public List<TBL_GROUP> ListAllGroup()
        //{
        //    return context.TBL_GROUPs.ToList().Select(t => new TBL_GROUP()
        //    {
        //        ID = t.ID,
        //        GROUPNAME = t.GROUPNAME,
        //    }).ToList();
            
        //}

        public void Add(TBL_LOGIN nhanvien)
        {
            nhanvien.ACTIVATE = true;
            nhanvien.ISADMIN = false;
            nhanvien.CREATED_BY = HttpContext.Current.User.Identity.Name;
            nhanvien.CREATED_DATE = DateTime.Now;
               
            context.TBL_LOGINs.Add(nhanvien);

            context.SaveChanges();
        }
        public List<TBL_GROUP> GetAllGroup()
        {
            return context.TBL_GROUPs.ToList();
        }
        public bool Update(TBL_LOGIN nhanvien)
        {

            TBL_LOGIN nv = context.TBL_LOGINs.Find(nhanvien.USERNAME);
            if (nv == null)
            {
                return false;
            }
            nv.PASSWORD = nhanvien.PASSWORD;
            nv.PHONE = nhanvien.PHONE;
            nv.EMAIL = nhanvien.EMAIL;
            nhanvien.UPDATED_BY = HttpContext.Current.User.Identity.Name;
            nhanvien.UPDATED_DATE = DateTime.Now;
            context.SaveChanges();
            return true;
        }

        public TBL_LOGIN FindById(string id)
        {
            return context.TBL_LOGINs.Find(id);
        }

        public bool Delete(TBL_LOGIN nhanvien)
        {

            TBL_LOGIN nv = context.TBL_LOGINs.Find(nhanvien.USERNAME);
            if (nv == null)
            {
                return false;
            }
            nv.ACTIVATE = false;
            context.SaveChanges();
            return true;
        }

        
    }
}