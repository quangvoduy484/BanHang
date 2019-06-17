using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class QuyenService
    {
        BanHangContext context = null;
        public QuyenService()
        {
            context = new BanHangContext() ;
        }

        public List<QuyenDropDownViewModel> GetAllDropDownList(string search, int idgroup)
        {
            //var nhanViens = context.TBL_LOGINs.Where(t => t.ACTIVATE != false);
            var Quyens = context.TBL_ROLEs
                 .Where(t => t.ACTIVATE != false)
                 .Where(t => t.TBL_GROUP_ROLEs.All(x => x.ID_GROUP != idgroup && x.ACTIVATE != false));     

            if (!string.IsNullOrWhiteSpace(search))
            {
                Quyens = Quyens.Where(t => t.ROLE_NAME.Contains(search));

            }
            var result = Quyens.OrderBy(t => t.ROLE_NAME)
                .Take(30)
                .Select(t => new QuyenDropDownViewModel()
                {
                    id = t.ID,
                    text = t.ROLE_NAME,
                    
                    
                }).ToList();

            return result;
        }

    }
}