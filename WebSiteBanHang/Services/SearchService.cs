using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;
using WebSiteBanHang.ViewModel;
using System.Data.Entity;
using System.Web.Mvc;

namespace WebSiteBanHang.Services
{
    public class SearchService
    {
        private BanHangContext context = null;
        public SearchService()
        {
            context = new BanHangContext();

        }
        //public RoomKindViewModel GetRoomKind(FormCollection fc)
        //{
        //    string tenSP = fc["txtsearch"];
        //    if (tenSP != null && tenSP != "")
        //    {
        //        var sannPham = (from sp in context.SANPHAMs
        //                        where sp.TenSanPham.ToUpper().Contains(tenSP.ToUpper())
        //                        orderby sp.Id_SanPham descending
        //                        select sp).Take(12).ToList();
        //    }
            


        //}
    }
}