using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return context.TBL_LOGINs.ToList().Select(t => new TBL_LOGIN()
            {
                USERNAME = t.USERNAME,
                 PASSWORD=t.PASSWORD,
                 EMAIL=t.EMAIL,
                 PHONE=t.PHONE,
                 ACTIVATE=t.ACTIVATE,
            }).ToList();
        }
        public bool Add(TBL_LOGIN model)
        {
            model.USERNAME = model.USERNAME.ToLower();
            var existUser = context.TBL_LOGINs.Where(t => t.USERNAME.ToLower() == model.USERNAME).FirstOrDefault();
            if (existUser != null)
                return false;
            
            model.ACTIVATE = true;
            
            context.TBL_LOGINs.Add(model);
            context.SaveChanges();
            return true;
        }


        public bool Delete(string userName)
        {
            TBL_LOGIN NVExist = context.TBL_LOGINs
                .FirstOrDefault(t=> t.USERNAME.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
            if (NVExist == null)
            {
                return false;
            }
            NVExist.ACTIVATE = !NVExist.ACTIVATE;
            context.SaveChanges();
            return true;
        }

    }
}