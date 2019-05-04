using System.Collections.Generic;
using System.Linq;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Services
{
    public class GiaSPService
    {
        private BanHangContext context = null;
        public GiaSPService()
        {
            context = new BanHangContext();

        }
        public List<GIASANPHAM> ListAll()
        {
            return context.GIASANPHAMs.ToList();
        }
        public List<GIASANPHAM> GetAllGiaSP()
        {
            return context.GIASANPHAMs.ToList();
        }
    }
}
