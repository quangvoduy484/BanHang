using System.Collections.Generic;
using System.Linq;
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
        public List<LOAISANPHAM> ListAll()
        {
            return context.LOAISANPHAMs.OrderBy(t => t.TenLoai).ToList();
        }
        public List<LOAIPHONG> GetAllLoaiPhong()
        {
            return context.LOAIPHONGs.ToList();
        }
    }
}
