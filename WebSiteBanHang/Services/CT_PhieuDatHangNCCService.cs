using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class CT_PhieuDatHangNCCService
    {
        private BanHangContext context = null;
        public CT_PhieuDatHangNCCService()
        {
            context = new BanHangContext();

        }
        public bool Add(CT_PhieuDatHangNCCViewModel model)
        {
            try
            {
                var dathang = new CT_PHIEUDATNCC
                {
                    MAPHIEUDAT=model.MaPhieuDat,
                    MASANPHAM=model.MaSP,
                    SOLUONG=model.SL,
                    GIANHAP=model.GiaNhap,
                    THANHTIEN=model.ThanhTien,
                };
                context.CT_PHIEUDATNCCs.Add(dathang);
                context.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
              
        }

        public bool Update(CT_PhieuDatHangNCCViewModel model)
        {

            CT_PHIEUDATNCC pdh = context.CT_PHIEUDATNCCs.FirstOrDefault(t => t.MAPHIEUDAT == model.MaPhieuDat);
            if (pdh == null)
            {
                return false;
            }
            pdh.MASANPHAM = model.MaSP;
            pdh.SOLUONG = model.SL;
            pdh.GIANHAP = model.GiaNhap;
            pdh.THANHTIEN = model.ThanhTien;

            context.SaveChanges();
            return true;
        }
    }
}