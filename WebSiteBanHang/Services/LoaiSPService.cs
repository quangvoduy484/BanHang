using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Areas.Admin.ViewModels;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class LoaiSPService
    {
        private BanHangContext context = null;
        public LoaiSPService()
        {
            context = new BanHangContext();

        }
        public List<LoaiSPViewModel> ListAll()
        {
            return context.LOAISANPHAMs.OrderBy(t => t.TenLoai)
                 .Select(t=> new LoaiSPViewModel
                 {
                     MaLoai = t.Id_LoaiPhong,
                     TenLoai = t.TenLoai
                 })
                .ToList();
        }
        public List<LOAIPHONG> GetAllLoaiPhong()
        {
            return context.LOAIPHONGs.ToList();
        }
    }
}
